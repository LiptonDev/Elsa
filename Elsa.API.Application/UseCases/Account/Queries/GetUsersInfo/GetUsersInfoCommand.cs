using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;

namespace Elsa.API.Application.UseCases.Account.Queries.GetUsersInfo;

/// <summary>
/// Команда получения информации о пользователях.
/// </summary>
public class GetUsersInfoCommand : GetUsersInfoRequest, IElsaRequestWrapper<List<GetMeResponse>>
{
}

/// <summary>
/// Обработчик получения информации о пользователях.
/// </summary>
public class GetUsersInfoCommandHandler : IElsaRequestHandlerWrapper<GetUsersInfoCommand, List<GetMeResponse>>
{
    private readonly IAccountService accountService;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public GetUsersInfoCommandHandler(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request">Данные для получения информации о пользователях.</param>
    public async Task<ServiceResult<List<GetMeResponse>>> Handle(GetUsersInfoCommand request, CancellationToken cancellationToken)
    {
        var result = await accountService.GetUsersInfoAsync(request.UserIds, cancellationToken);
        return new ServiceResult<List<GetMeResponse>>(result);
    }
}
