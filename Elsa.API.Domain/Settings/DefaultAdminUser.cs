namespace Elsa.API.Domain.Settings;

/// <summary>
/// Стандартный аккаунт администратора.
/// </summary>
public class DefaultAdminUser
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
    /// Никнейм.
    /// </summary>
    public string UserName { get; set; }
}
