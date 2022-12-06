using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;

namespace Elsa.API.Application.UseCases.Account.Queries.GetPasswordResetToken;

/// <summary>
/// Команда запроса токена сброса пароля.
/// </summary>
public class GetResetPasswordTokenCommand : ResetPasswordGetTokenRequest, IElsaRequestWrapper<ResetPasswordGetTokenResponse>
{
}

/// <summary>
/// Обработчик запроса токена сброса пароля.
/// </summary>
public class GetResetPasswordTokenCommandHandler : IElsaRequestHandlerWrapper<GetResetPasswordTokenCommand, ResetPasswordGetTokenResponse>
{
    private readonly IAccountService accountService;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public GetResetPasswordTokenCommandHandler(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request">Данные для получения токена.</param>
    public async Task<ServiceResult<ResetPasswordGetTokenResponse>> Handle(GetResetPasswordTokenCommand request, CancellationToken cancellationToken)
    {
        var res = await accountService.SendResetPasswordTokenAsync(request.Email, cancellationToken);
        return new ServiceResult<ResetPasswordGetTokenResponse>(res);
    }
}
