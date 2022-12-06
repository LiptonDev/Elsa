using Elsa.API.Domain.Common;

namespace Elsa.API.Domain.Events.Account;

/// <summary>
/// Токен был удален.
/// </summary>
public class TokensWasRemovedEvent : DomainEvent
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public TokensWasRemovedEvent(string[] tokens)
    {
        Tokens = tokens;
    }

    /// <summary>
    /// Удаленные токены.
    /// </summary>
    public string[] Tokens { get; }
}
