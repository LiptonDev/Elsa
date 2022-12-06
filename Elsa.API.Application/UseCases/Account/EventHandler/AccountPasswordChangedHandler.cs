using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Application.UseCases.Account.Commands.Delete;
using Elsa.API.Domain.Events.Account;
using Elsa.Core.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Elsa.API.Application.UseCases.Account.EventHandler;

/// <summary>
/// Обработчик события измненения пароля пользователя.
/// </summary>
public class AccountPasswordChangedHandler : INotificationHandler<DomainEventNotification<AccountPasswordChangedEvent>>
{
    private readonly IAccountService accountService;
    private readonly IDomainEventsService publisher;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public AccountPasswordChangedHandler(IAccountService accountService, IDomainEventsService publisher)
    {
        this.accountService = accountService;
        this.publisher = publisher;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="notification">Событие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public async Task Handle(DomainEventNotification<AccountPasswordChangedEvent> notification, CancellationToken cancellationToken)
    {
        var tokens = await accountService.RemoveTokenAsync(new LogoutCommand
        {
            UserId = notification.DomainEvent.UserId,
            RemoveType = RemoveTokenType.All
        }, cancellationToken);
        if (tokens.Length > 0)
        {
            await publisher.PublishAsync(new TokensWasRemovedEvent(tokens), cancellationToken);
        }
    }
}
