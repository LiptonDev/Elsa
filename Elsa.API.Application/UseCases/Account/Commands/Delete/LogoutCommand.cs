using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Domain.Events.Account;
using Elsa.Core.Enums;
using Elsa.Core.Models.Account.Request;
using Elsa.Core.Models.Account.Response;

namespace Elsa.API.Application.UseCases.Account.Commands.Delete;

/// <summary>
/// Команда выхода из системы.
/// </summary>
public class LogoutCommand : LogoutRequest, IElsaRequestWrapper<LogoutResponse>
{
    /// <summary>
    /// Id пользователя, который вышел из системы.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Текущий токен из запроса (используется только для <see cref="RemoveTokenType.JustCurrent"/> и <see cref="RemoveTokenType.AllExceptCurrent"/>).
    /// </summary>
    public string? Token { get; set; }
}

/// <summary>
/// Обработчик выхода из системы.
/// </summary>
public class LogoutCommandHandler : IElsaRequestHandlerWrapper<LogoutCommand, LogoutResponse>
{
    private readonly IAccountService accountService;
    private readonly IDomainEventsService publisher;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public LogoutCommandHandler(IAccountService accountService, IDomainEventsService publisher)
    {
        this.accountService = accountService;
        this.publisher = publisher;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="request">Данные для выхода.</param>
    public async Task<ServiceResult<LogoutResponse>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var result = await accountService.RemoveTokenAsync(request, cancellationToken);
        if (result.Length > 0)
        {
            await publisher.PublishAsync(new TokensWasRemovedEvent(result), cancellationToken);
        }
        return new ServiceResult<LogoutResponse>(new LogoutResponse { Count = result.Length });
    }
}