using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.UseCases.Account.Commands.Create;
using Elsa.API.Application.UseCases.Account.Queries;
using Elsa.Core.Models.Account;
using Elsa.Core.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Elsa.API.Controllers;

/// <summary>
/// Контроллер аккаунтов.
/// </summary>
public class AccountController : BaseController
{
    /// <summary>
    /// Получить информацию о нескольких пользователя.
    /// </summary>
    /// <param name="accountService"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet(nameof(GetUsersInfo))]
    [ProducesResponseType(typeof(ElsaResult<List<GetMeResponse>>), 200)]
    [ProducesResponseType(typeof(ElsaResult), 401)]
    [ProducesResponseType(typeof(ElsaResult), 403)]
    public async Task<ElsaResult<List<GetMeResponse>>> GetUsersInfo([FromServices] IAccountService accountService,
                                                                       [FromBody] GetUsersInfoRequest request)
    {
        var res = await accountService.GetUsersInfoAsync(request.UserIds);
        return new ElsaResult<List<GetMeResponse>>(res);
    }

    /// <summary>
    /// Получить информацию обо мне.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet(nameof(GetMe))]
    [ProducesResponseType(typeof(ElsaResult<GetMeResponse>), 200)]
    [ProducesResponseType(typeof(ElsaResult), 401)]
    public async Task<ElsaResult<GetMeResponse>> GetMe([FromServices] ICurrentUserService currentUser,
                                                          [FromServices] IAccountService accountService)
    {
        var res = await accountService.GetUsersInfoAsync(new[] { currentUser.UserId });
        return new ElsaResult<GetMeResponse>(res.FirstOrDefault());
    }

    /// <summary>
    /// Получение списка ролей.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet(nameof(GetRoles))]
    [ProducesResponseType(typeof(ElsaResult<string[]>), 200)]
    [ProducesResponseType(typeof(ElsaResult), 401)]
    public Task<ElsaResult<string[]>> GetRoles([FromServices] ICurrentUserService currentUser)
    {
        return Task.FromResult(new ElsaResult<string[]>(currentUser.Roles));
    }

    /// <summary>
    /// Выход из системы (удаление текущего токена).
    /// </summary>
    /// <param name="removeTokenType">Тип удаления токен(а/ов).</param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete(nameof(Logout))]
    [ProducesResponseType(typeof(ElsaResult), 401)]
    public Task Logout([FromServices] ICurrentUserService currentUser,
                       [FromServices] IAccountService accountService,
                       [FromBody] LogoutRequest removeTokenType)
    {
        var token = base.ControllerContext.HttpContext.Request.Headers[ElsaSchemeConsts.SchemeBearer];
        return accountService.RemoveTokenAsync(currentUser.UserId, token, removeTokenType.RemoveType);
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="request">Модель регистрации.</param>
    /// <returns></returns>
    [HttpPost(nameof(Register))]
    [ProducesResponseType(typeof(ElsaResult<RegisterResponse>), 200)]
    [ProducesResponseType(typeof(ElsaResult), 400)]
    public Task<ElsaResult<RegisterResponse>> Register([FromBody] CreateUserCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="request">Данные для авторизации.</param>
    /// <returns></returns>
    [HttpPost(nameof(Login))]
    [ProducesResponseType(typeof(ElsaResult<LoginResponse>), 200)]
    [ProducesResponseType(typeof(ElsaResult), 400)]
    public Task<ElsaResult<LoginResponse>> Login([FromBody] GetAccessTokenCommand request)
    {
        return Mediator.Send(request);
    }
}
