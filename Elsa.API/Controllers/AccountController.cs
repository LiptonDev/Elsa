using AutoMapper;
using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Commands.Delete;
using Elsa.API.Application.UseCases.Account.Commands.Update;
using Elsa.API.Application.UseCases.Account.Queries.GetPasswordResetToken;
using Elsa.API.Application.UseCases.Account.Queries.GetToken;
using Elsa.API.Application.UseCases.Account.Queries.GetUsersInfo;
using Elsa.Core.Enums;
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
    public Task<ServiceResult<ResetPasswordGetTokenResponse>> GetResetPasswordToken([FromBody] GetResetPasswordTokenCommand request,
                                                                                    CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    /// <summary>
    /// Получить информацию о нескольких пользователя.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet(nameof(GetUsersInfo))]
    public Task<ServiceResult<List<GetMeResponse>>> GetUsersInfo([FromBody] GetUsersInfoCommand request,
                                                                 CancellationToken cancellationToken)
    {
        return Mediator.Send(request, cancellationToken);
    }

    /// <summary>
    /// Получить информацию обо мне.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet(nameof(GetMe))]
    public async Task<ServiceResult<GetMeResponse>> GetMe([FromServices] ICurrentUserService currentUser,
                                                          [FromServices] IAccountService accountService,
                                                          CancellationToken cancellationToken)
    {
        var res = await accountService.GetUsersInfoAsync(new[] { currentUser.UserId! }, cancellationToken);
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
        return Task.FromResult(new ServiceResult<string[]>(currentUser.Roles!));
    }

    /// <summary>
    /// Удалить токен(ы) пользователя.
    /// </summary>
    /// <param name="request">Данные для удаления токенов.</param>
    /// <returns></returns>
    [Authorize(Roles = nameof(Roles.Admin))]
    [HttpDelete(nameof(DeleteUserTokens))]
    public Task<ServiceResult<LogoutResponse>> DeleteUserTokens([FromServices] IMapper mapper,
                                                                [FromBody] DeleteUserTokensRequest request,
                                                                CancellationToken cancellationToken)
    {
        var map = mapper.Map<LogoutCommand>(request);
        return Mediator.Send(map, cancellationToken);
    }

    /// <summary>
    /// Выход из системы (удаление токен(а/ов)).
    /// </summary>
    /// <param name="request">Тип удаления токен(а/ов).</param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete(nameof(Logout))]
    public Task<ServiceResult<LogoutResponse>> Logout([FromServices] ICurrentUserService currentUser,
                                                      [FromServices] IMapper mapper,
                                                      [FromBody] LogoutRequest request,
                                                      CancellationToken cancellationToken)
    {
        var token = base.ControllerContext.HttpContext.Request.Headers[ElsaSchemeConsts.SchemeBearer];
        var map = mapper.Map<LogoutCommand>(request);
        map.Token = token!;
        map.UserId = currentUser.UserId!;
        return Mediator.Send(map, cancellationToken);
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
