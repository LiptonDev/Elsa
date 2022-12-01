using AutoMapper;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Queries;
using Elsa.API.Infrastructure.Persistence;
using Elsa.API.Infrastructure.Projections;
using Elsa.Core.Enums;
using Elsa.Core.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Elsa.API.Infrastructure.Services;

/// <inheritdoc cref="IAccountService"/>
public class AccountService : IAccountService
{
    private readonly UserManager<ElsaUser> userManager;
    private readonly ElsaDbContext dbContext;
    private readonly ILookupNormalizer normalizer;
    private readonly ITokenGenerator tokenGenerator;
    private readonly IMapper mapper;

    public AccountService(UserManager<ElsaUser> userManager,
                          ElsaDbContext dbContext,
                          ILookupNormalizer normalizer,
                          ITokenGenerator tokenGenerator,
                          IMapper mapper)
    {
        this.userManager = userManager;
        this.dbContext = dbContext;
        this.normalizer = normalizer;
        this.tokenGenerator = tokenGenerator;
        this.mapper = mapper;
    }

    public Task<bool> CheckEmailAsync(string email)
    {
        email = normalizer.NormalizeEmail(email);
        return userManager.Users.AnyAsync(x => x.NormalizedEmail == email);
    }

    public async Task<List<GetMeResponse>> GetUsersInfoAsync(string[] userIds)
    {
        var users = mapper.ProjectTo<GetMeProjection>(userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role)).AsQueryable();
        if (userIds is not null && userIds.Length > 0)
        {
            users = users.Where(x => userIds.Contains(x.Id));
        }
        return mapper.Map<List<GetMeResponse>>(users);
    }

    public async Task<RegisterResponse> RegisterAsync(CreateUserCommand request)
    {
        var res = await userManager.CreateAsync(new ElsaUser
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        }, request.Password);

        var errors = res.Errors.GroupBy(x => x.Code, x => x.Description).Select(x => new RegisterError { ErrorCode = x.Key, Errors = x.ToArray() }).ToList();
        return new RegisterResponse { Succeeded = res.Succeeded, Errors = errors };
    }

    public async Task RemoveTokenAsync(string userId, string currentToken, RemoveTokenType removeType)
    {
        if (removeType == RemoveTokenType.All || removeType == RemoveTokenType.AllExceptCurrent)
        {
            var tokens = dbContext.ApiKeys.AsQueryable();
            if (removeType == RemoveTokenType.All)
            {
                tokens = tokens.Where(x => x.UserId == userId);
            }
            else
            {
                tokens = tokens.Where(x => x.UserId == userId && x.Key != currentToken);
            }
            dbContext.ApiKeys.RemoveRange(tokens);
            await dbContext.SaveChangesAsync();
        }
        else if (removeType == RemoveTokenType.JustCurrent)
        {
            var token = await dbContext.ApiKeys.FirstOrDefaultAsync(x => x.UserId == userId && x.Key == currentToken);
            dbContext.ApiKeys.Remove(token);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<LoginResponse?> TryLoginAsync(GetAccessTokenCommand request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return null;
        }
        var check = await userManager.CheckPasswordAsync(user, request.Password);
        if (check)
        {
            var key = tokenGenerator.GenerateToken();
            user.ApiKeys.Add(new ElsaApiKey { Key = key });
            await userManager.UpdateAsync(user);

            return new LoginResponse { ApiToken = key };
        }
        return null;
    }
}
