using Elsa.API.Domain.Common;

namespace Elsa.API.Domain.Events.Account;

/// <summary>
/// Событие изменения пароля аккаунта.
/// </summary>
public class AccountPasswordChanged : DomainEvent
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public AccountPasswordChanged(string userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Id пользователя, у которого был изменен пароль.
    /// </summary>
    public string UserId { get; }
}
