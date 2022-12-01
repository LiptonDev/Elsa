using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.Core.Enums;
using Elsa.Core.Models.Account;
using Elsa.Core.Models.Response;
using Microsoft.Extensions.Localization;
using System.Net;

namespace Elsa.API.Application.UseCases.Account.Commands.Create;

/// <summary>
/// Модель для команды регистрации.
/// </summary>
public class CreateUserCommand : RegisterRequest, IElsaRequestWrapper<RegisterResponse> { }

/// <summary>
/// Обработчик регистрации пользователя.
/// </summary>
class CreateUserCommandHandler : IElsaRequestHandlerWrapper<CreateUserCommand, RegisterResponse>
{
    private readonly IAccountService accountService;
    private readonly IStringLocalizer<IdentityStrings> localizer;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public CreateUserCommandHandler(IAccountService accountService, IStringLocalizer<IdentityStrings> localizer)
    {
        this.accountService = accountService;
        this.localizer = localizer;
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="request">Данные для регистрации.</param>
    /// <returns></returns>
    public async Task<ElsaResult<RegisterResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var res = await accountService.RegisterAsync(request);
        if (res.Succeeded)
        {
            return new ServiceResult<RegisterResponse>(res);
        }
        else
        {
            return new ServiceResult<RegisterResponse>(res, new ElsaError(localizer[IdentityStrings.RegistrationFailed], ErrorCode.Unauthorized), HttpStatusCode.Unauthorized);
        }
    }
}
