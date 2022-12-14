namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Запрос данных о пользователях.
/// </summary>
public class GetUsersInfoRequest
{
    /// <summary>
    /// Id'ы пользователей.
    /// </summary>
    public required string[] UserIds { get; set; }
}
