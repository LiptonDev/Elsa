namespace Elsa.Core.Models.Account.Request;

/// <summary>
/// Данные для регистрации.
/// </summary>
public class RegisterRequest
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
    /// Пароль.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Подтверждение пароля.
    /// </summary>
    public string ConfirmPassword { get; set; }
}
