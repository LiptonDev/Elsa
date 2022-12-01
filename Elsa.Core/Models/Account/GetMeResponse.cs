namespace Elsa.Core.Models.Account;

/// <summary>
/// Данные обо мне.
/// </summary>
public class GetMeResponse
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Почта.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Роли.
    /// </summary>
    public string[]? Roles { get; set; }
}
