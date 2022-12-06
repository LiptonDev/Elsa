namespace Elsa.Core.Models.Account.Response;

/// <summary>
/// Данные обо мне.
/// </summary>
public class GetMeResponse
{
    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Почта.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Роли.
    /// </summary>
    public required string[] Roles { get; set; }
}
