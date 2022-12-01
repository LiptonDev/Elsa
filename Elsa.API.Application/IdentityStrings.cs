namespace Elsa.API.Application;

/// <summary>
/// Локализация строк.
/// </summary>
public class IdentityStrings
{
    /// <summary>
    /// Почта уже используется.
    /// </summary>
    public const string EmailIsInUse = nameof(EmailIsInUse);

    /// <summary>
    /// Пароли не совпадают.
    /// </summary>
    public const string ConfirmPassword = nameof(ConfirmPassword);

    /// <summary>
    /// Пользователь не найден (почта или пароль не подошли).
    /// </summary>
    public const string UserNotFound = nameof(UserNotFound);

    /// <summary>
    /// Ошибка регистрации.
    /// </summary>
    public const string RegistrationFailed = nameof(RegistrationFailed);

    /// <summary>
    /// Ошибка удаления токена.
    /// </summary>
    public const string RemoveTokenFailed = nameof(RemoveTokenFailed);
}
