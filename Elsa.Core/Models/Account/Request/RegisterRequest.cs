namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Данные для регистрации.
/// </summary>
public class RegisterRequest
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
    /// Пароль.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Подтверждение пароля.
    /// </summary>
    public required string ConfirmPassword { get; set; }
}
