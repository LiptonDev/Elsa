using Elsa.Core.Enums;

namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Запрос выхода из системы (удаление токена или токенов).
/// </summary>
public class LogoutRequest
{
    /// <summary>
    /// Тип удаления токена.
    /// </summary>
    public required RemoveTokenType RemoveType { get; set; }
}
