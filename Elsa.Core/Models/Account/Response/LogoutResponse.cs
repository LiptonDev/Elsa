namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Результат выхода (удаления токенов).
/// </summary>
public class LogoutResponse
{
    /// <summary>
    /// Количество удаленных токенов.
    /// </summary>
    public required int Count { get; set; }
}
