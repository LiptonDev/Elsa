namespace Elsa.Core.Models.Account;

/// <summary>
/// Запрос данных о пользователях.
/// </summary>
public class GetUsersInfoRequest
{
    /// <summary>
    /// Id'ы пользователей.
    /// </summary>
    public string[] UserIds { get; set; }
}
