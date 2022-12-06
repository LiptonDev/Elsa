using AutoMapper;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Extensions;
using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Commands.Delete;
using Elsa.API.Application.UseCases.Account.Queries.GetToken;
using Elsa.API.Domain.Entities;
using Elsa.API.Domain.Settings;
using Elsa.API.Infrastructure.Projections;
using Elsa.Core.Enums;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Elsa.API.Infrastructure.Services;

/// <inheritdoc cref="IAccountService"/>
public class AccountService : IAccountService
{
    #region Localization
    /// <summary>
    /// Строка локализации заголовка письма.
    /// </summary>
    string ResetPasswordTokenSubject { get; } = nameof(ResetPasswordTokenSubject);

    /// <summary>
    /// Строка локализации текста письма.
    /// </summary>
    string ResetPasswordTokenBody { get; } = nameof(ResetPasswordTokenBody);
    #endregion

    private readonly UserManager<ElsaUser> userManager;
    private readonly IUnitOfWork<int> unitOfWork;
    private readonly ILookupNormalizer normalizer;
    private readonly ITokenGenerator tokenGenerator;
    private readonly IStringLocalizer<AccountService> localizer;
    private readonly ResetTokenMailBodySettings settings;
    private readonly IMapper mapper;

    public AccountService(UserManager<ElsaUser> userManager,
                          IUnitOfWork<int> unitOfWork,
                          ILookupNormalizer normalizer,
                          ITokenGenerator tokenGenerator,
                          IStringLocalizer<AccountService> localizer,
                          IOptions<ResetTokenMailBodySettings> settings,
                          IMapper mapper)
    {
        this.userManager = userManager;
        this.unitOfWork = unitOfWork;
        this.normalizer = normalizer;
        this.tokenGenerator = tokenGenerator;
        this.localizer = localizer;
        this.settings = settings.Value;
        this.mapper = mapper;
    }

    public Task<bool> CheckEmailAsync(string email, CancellationToken cancellationToken)
    {
        email = normalizer.NormalizeEmail(email);
        return userManager.Users.AnyAsync(x => x.NormalizedEmail == email, cancellationToken);
    }

    public async Task<List<GetMeResponse>> GetUsersInfoAsync(string[] userIds, CancellationToken cancellationToken)
    {
        var users = mapper.ProjectTo<GetMeProjection>(userManager.Users.AsNoTracking());
        if (userIds is not null && userIds.Length > 0)
        {
            users = users.Where(x => userIds.Contains(x.Id));
        }
        var res = await users.ToListAsync(cancellationToken);
        return mapper.Map<List<GetMeResponse>>(res);
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

        return new RegisterResponse { Succeeded = res.Succeeded, Errors = res.Errors.ToElsaErrors() };
    }

    public async Task<string[]> RemoveTokenAsync(LogoutCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<ElsaApiKey>();
        var tokens = repo.Entities().Where(x => x.UserId == request.UserId);
        switch (request.RemoveType)
        {
            case RemoveTokenType.JustCurrent:
                tokens = tokens.Where(x => x.Key == request.Token);
                break;
            case RemoveTokenType.AllExceptCurrent:
                tokens = tokens.Where(x => x.Key != request.Token);
                break;
        }
        var results = await tokens.Select(x => x.Key).ToArrayAsync(cancellationToken);
        if (results.Length > 0)
        {
            await tokens.ExecuteDeleteAsync(cancellationToken);
        }
        return results!;
    }

    public async Task<(string?, ResetPasswordResponse)> ResetPasswordAsync(ResetPasswordRequest request, bool useRequestToken, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return (null, new ResetPasswordResponse { Succeeded = false });
        }

        request.ResetToken = useRequestToken ? request.ResetToken : await userManager.GeneratePasswordResetTokenAsync(user);

        var reset = await userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);
        return (user.Id, new ResetPasswordResponse
        {
            Succeeded = reset.Succeeded,
            Errors = reset.Errors.ToElsaErrors()
        });
    }

    public async Task<ResetPasswordGetTokenResponse> SendResetPasswordTokenAsync(string email, CancellationToken cancellationToken)
    {
        var response = new ResetPasswordGetTokenResponse { Succeeded = true };
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return response;
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var body = settings.Body.Replace(ResetTokenMailBodySettings.LocalizeText, localizer[ResetPasswordTokenBody])
                                .Replace(ResetTokenMailBodySettings.TokenText, token);

        await unitOfWork.Repository<EmailEntity>().AddAsync(new EmailEntity
        {
            Body = body,
            To = user.Email!,
            Subject = localizer[ResetPasswordTokenSubject]
        }, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        return response;
    }

    public async Task<LoginResponse?> TryLoginAsync(GetAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return null;

        var check = await userManager.CheckPasswordAsync(user, request.Password);
        if (!check)
            return null;

        var key = await tokenGenerator.GenerateTokenAsync(cancellationToken);
        var apiKey = new ElsaApiKey { Key = key, User = user };
        await unitOfWork.Repository<ElsaApiKey>().AddAsync(apiKey, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        return new LoginResponse { ApiToken = key };
    }
}
