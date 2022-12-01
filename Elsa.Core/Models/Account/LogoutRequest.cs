using Elsa.Core.Enums;

namespace Elsa.Core.Models.Account;

/// <summary>
/// Запрос выхода из системы (удаление токена или токенов).
/// </summary>
public class LogoutRequest
{
    /// <summary>
    /// Тип удаления токена.
    /// </summary>
    public RemoveTokenType RemoveType { get; set; }
}
