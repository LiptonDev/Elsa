using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Application.Common.Models;
using Elsa.API.Domain.Events.Account;
using Elsa.Core.Enums;
using MediatR;

namespace Elsa.API.Application.UseCases.Account.EventHandler;

/// <summary>
/// Обработчик события измненения пароля пользователя.
/// </summary>
public class AccountPasswordChangedHandler : INotificationHandler<DomainEventNotification<AccountPasswordChanged>>
{
    private readonly IAccountService accountService;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public AccountPasswordChangedHandler(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    /// <summary>
    /// Обработчик.
    /// </summary>
    /// <param name="notification">Событие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    public Task Handle(DomainEventNotification<AccountPasswordChanged> notification, CancellationToken cancellationToken)
    {
        return accountService.RemoveTokenAsync(notification.DomainEvent.UserId, null, RemoveTokenType.All, cancellationToken);
    }
}
