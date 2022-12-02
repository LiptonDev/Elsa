using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.Core.Enums;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;
using Elsa.Core.Models.Response;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Elsa.API.Application.UseCases.Account.Queries;

/// <summary>
/// Модель для команды авторизации.
/// </summary>
public class GetAccessTokenCommand : LoginRequest, IElsaRequestWrapper<LoginResponse> { }

/// <summary>
/// Команда получения токена авторизации.
/// </summary>
class GetAccessTokenCommandHandler : IElsaRequestHandlerWrapper<GetAccessTokenCommand, LoginResponse>
{
    private readonly IAccountService accountService;
    private readonly IStringLocalizer<IdentityStrings> stringLocalizer;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public GetAccessTokenCommandHandler(IAccountService accountService, IStringLocalizer<IdentityStrings> stringLocalizer)
    {
        this.accountService = accountService;
        this.stringLocalizer = stringLocalizer;
    }

    /// <summary>
    /// Получение токена авторизации.
    /// </summary>
    /// <param name="request">Данные для авторизации.</param>
    /// <returns></returns>
    public async Task<ServiceResult<LoginResponse>> Handle(GetAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var res = await accountService.TryLoginAsync(request, cancellationToken);
        if (res != null)
        {
            return new ServiceResult<LoginResponse>(res);
        }
        else
        {
            return new ServiceResult<LoginResponse>(new ElsaError(stringLocalizer[IdentityStrings.UserNotFound], ErrorCode.Unauthorized), HttpStatusCode.Unauthorized);
        }
    }
}
