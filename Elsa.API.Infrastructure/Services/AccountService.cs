using AutoMapper;
using Elsa.API.Application.Common.Exceptions;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Extensions;
using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Queries;
using Elsa.API.Domain.Entities;
using Elsa.API.Infrastructure.Projections;
using Elsa.Core.Enums;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Elsa.API.Infrastructure.Services;

/// <inheritdoc cref="IAccountService"/>
public class AccountService : IAccountService
{
    #region Localization
    /// <summary>
    /// Строка локализации, если тип удаления токена передан не правильно.
    /// </summary>
    const string RemoveTokenTypeInvalid = nameof(RemoveTokenTypeInvalid);

    /// <summary>
    /// Строка локализации для отправки токена сброса пароля.
    /// </summary>
    const string SendResetPasswordToken = nameof(SendResetPasswordToken);

    /// <summary>
    /// Строка локализации заголовка письма.
    /// </summary>
    const string ResetPasswordTokenSubject = nameof(ResetPasswordTokenSubject);

    /// <summary>
    /// Строка локализации текста письма.
    /// </summary>
    const string ResetPasswordTokenBody = nameof(ResetPasswordTokenBody);

    /// <summary>
    /// Строка локализации при смене пароля.
    /// </summary>
    const string ResetPassword = nameof(ResetPassword);

    /// <summary>
    /// Строка локализации при ошибке смены пароля.
    /// </summary>
    const string ResetPasswordError = nameof(ResetPasswordError);
    #endregion

    private readonly UserManager<ElsaUser> userManager;
    private readonly IUnitOfWork<int> unitOfWork;
    private readonly ILookupNormalizer normalizer;
    private readonly ITokenGenerator tokenGenerator;
    private readonly IStringLocalizer<AccountService> localizer;
    private readonly IMapper mapper;

    public AccountService(UserManager<ElsaUser> userManager,
                          IUnitOfWork<int> unitOfWork,
                          ILookupNormalizer normalizer,
                          ITokenGenerator tokenGenerator,
                          IStringLocalizer<AccountService> localizer,
                          IMapper mapper)
    {
        this.userManager = userManager;
        this.unitOfWork = unitOfWork;
        this.normalizer = normalizer;
        this.tokenGenerator = tokenGenerator;
        this.localizer = localizer;
        this.mapper = mapper;
    }

    public Task<bool> CheckEmailAsync(string email)
    {
        email = normalizer.NormalizeEmail(email);
        return userManager.Users.AnyAsync(x => x.NormalizedEmail == email);
    }

    public async Task<List<GetMeResponse>> GetUsersInfoAsync(string[] userIds)
    {
        var users = mapper.ProjectTo<GetMeProjection>(userManager.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role)).AsQueryable().AsNoTracking();
        if (userIds is not null && userIds.Length > 0)
        {
            users = users.Where(x => userIds.Contains(x.Id));
        }
        var res = await users.ToListAsync();
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

    public Task<int> RemoveTokenAsync(string userId, string currentToken, RemoveTokenType removeType, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.Repository<ElsaApiKey>();
        if (removeType == RemoveTokenType.All || removeType == RemoveTokenType.AllExceptCurrent)
        {
            var tokens = repo.Entities.AsQueryable();
            if (removeType == RemoveTokenType.All)
            {
                tokens = tokens.Where(x => x.UserId == userId);
                return tokens.ExecuteDeleteAsync(cancellationToken);
            }
            else
            {
                tokens = tokens.Where(x => x.UserId == userId && x.Key != currentToken);
                return tokens.ExecuteDeleteAsync(cancellationToken);
            }
        }
        else if (removeType == RemoveTokenType.JustCurrent)
        {
            var token = repo.Entities.Where(x => x.UserId == userId && x.Key == currentToken);
            return token.ExecuteDeleteAsync(cancellationToken);
        }
        else
        {
            throw new ElsaApiException(localizer[RemoveTokenTypeInvalid], ErrorCode.Validation, HttpStatusCode.BadRequest);
        }
    }

    public async Task<(string?, ResetPasswordResponse)> ResetPasswordAsync(ResetPasswordRequest request, bool useRequestToken, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return (null, new ResetPasswordResponse { Message = localizer[ResetPasswordError] });
        }

        request.ResetToken = useRequestToken ? request.ResetToken : await userManager.GeneratePasswordResetTokenAsync(user);

        var reset = await userManager.ResetPasswordAsync(user, request.ResetToken, request.NewPassword);
        return (user.Id, new ResetPasswordResponse
        {
            Succeeded = reset.Succeeded,
            Message = reset.Succeeded ? localizer[ResetPassword] : localizer[ResetPasswordError],
            Errors = reset.Errors.ToElsaErrors()
        });
    }

    public async Task<ResetPasswordGetTokenResponse> SendResetPasswordTokenAsync(string email, CancellationToken cancellationToken)
    {
        var response = new ResetPasswordGetTokenResponse { Message = localizer[SendResetPasswordToken] };
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
            return response;
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        await unitOfWork.Repository<EmailEntity>().AddAsync(new EmailEntity
        {
            Body = string.Format(localizer[ResetPasswordTokenBody], token),
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

        var key = await tokenGenerator.GenerateTokenAsync();
        var apiKey = new ElsaApiKey { Key = key, User = user };
        await unitOfWork.Repository<ElsaApiKey>().AddAsync(apiKey, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        return new LoginResponse { ApiToken = key };
    }
}
