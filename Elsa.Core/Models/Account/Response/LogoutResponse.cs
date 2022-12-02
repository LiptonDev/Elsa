namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Результат выхода (удаления токенов).
/// </summary>
public class LogoutResponse
{
    /// <summary>
    /// Количество удаленных токенов.
    /// </summary>
    public int Count { get; set; }
}
