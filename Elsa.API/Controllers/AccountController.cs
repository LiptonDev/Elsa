using AutoMapper;
using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Commands.Update;
using Elsa.API.Application.UseCases.Account.Queries;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.API.Controllers;

/// <summary>
/// Контроллер аккаунтов.
/// </summary>
public class AccountController : BaseController
{
    /// <summary>
    /// Установить пароль для пользователя.
    /// </summary>
    /// <param name="request">Данные для установки пароля.</param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPost(nameof(SetUserPassword))]
    public Task<ServiceResult<ResetPasswordResponse>> SetUserPassword([FromServices] IMapper mapper,
                                                                      [FromBody] ResetPasswordRequest request,
                                                                      CancellationToken cancellationToken)
    {
        var send = mapper.Map<UpdateUserPasswordCommand>(request);
        send.IsAdminAction = true;
        return Mediator.Send(send, cancellationToken);
    }

    /// <summary>
    /// Смена пароля.
    /// </summary>
    /// <param name="request">Данные для сброса пароля.</param>
    /// <returns></returns>
    [HttpPost(nameof(ResetPassword))]
    public Task<ServiceResult<ResetPasswordResponse>> ResetPassword([FromServices] IMapper mapper,
                                                                    [FromBody] ResetPasswordRequest request,
                                                                    CancellationToken cancellationToken)
    {
        var send = mapper.Map<UpdateUserPasswordCommand>(request);
        send.IsAdminAction = false;
        return Mediator.Send(send, cancellationToken);
    }

    /// <summary>
    /// Отправить токен обновления пароля на почту.
    /// </summary>
    /// <returns></returns>
    [HttpPost(nameof(GetResetPasswordToken))]
    public async Task<ServiceResult<ResetPasswordGetTokenResponse>> GetResetPasswordToken([FromServices] IAccountService accountService,
                                                                                          [FromBody] ResetPasswordGetTokenRequest request,
                                                                                          CancellationToken cancellationToken)
    {
        var res = await accountService.SendResetPasswordTokenAsync(request.Email, cancellationToken);
        return new ServiceResult<ResetPasswordGetTokenResponse>(res);
    }

    /// <summary>
    /// Получить информацию о нескольких пользователя.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet(nameof(GetUsersInfo))]
    public async Task<ServiceResult<List<GetMeResponse>>> GetUsersInfo([FromServices] IAccountService accountService,
                                                                       [FromBody] GetUsersInfoRequest request)
    {
        var res = await accountService.GetUsersInfoAsync(request.UserIds);
        return new ServiceResult<List<GetMeResponse>>(res);
    }

    /// <summary>
    /// Получить информацию обо мне.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet(nameof(GetMe))]
    public async Task<ServiceResult<GetMeResponse>> GetMe([FromServices] ICurrentUserService currentUser,
                                                          [FromServices] IAccountService accountService)
    {
        var res = await accountService.GetUsersInfoAsync(new[] { currentUser.UserId });
        return new ServiceResult<GetMeResponse>(res.FirstOrDefault());
    }

    /// <summary>
    /// Получение списка ролей.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet(nameof(GetRoles))]
    public Task<ServiceResult<string[]>> GetRoles([FromServices] ICurrentUserService currentUser)
    {
        return Task.FromResult(new ServiceResult<string[]>(currentUser.Roles));
    }

    /// <summary>
    /// Выход из системы (удаление токен(а/ов)).
    /// </summary>
    /// <param name="removeTokenType">Тип удаления токен(а/ов).</param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete(nameof(Logout))]
    public async Task<ServiceResult<LogoutResponse>> Logout([FromServices] ICurrentUserService currentUser,
                                                            [FromServices] IAccountService accountService,
                                                            [FromBody] LogoutRequest removeTokenType,
                                                            CancellationToken cancellationToken)
    {
        var token = base.ControllerContext.HttpContext.Request.Headers[ElsaSchemeConsts.SchemeBearer];
        var res = await accountService.RemoveTokenAsync(currentUser.UserId, token, removeTokenType.RemoveType, cancellationToken);
        return new ServiceResult<LogoutResponse>(new LogoutResponse { Count = res });
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="request">Модель регистрации.</param>
    /// <returns></returns>
    [HttpPost(nameof(Register))]
    public Task<ServiceResult<RegisterResponse>> Register([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="request">Данные для авторизации.</param>
    /// <returns></returns>
    [HttpPost(nameof(Login))]
    public Task<ServiceResult<LoginResponse>> Login([FromBody] GetAccessTokenCommand request, CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }
}
