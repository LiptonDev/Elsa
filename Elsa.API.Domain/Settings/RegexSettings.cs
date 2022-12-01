namespace Elsa.API.Domain.Settings;

/// <summary>
/// Настройки регулярных выражений.
/// </summary>
public class RegexSettings
{
    /// <summary>
    /// Regex паттерн для валидации имени пользователя.
    /// </summary>
    public string UserFirstNamePattern { get; set; }

    /// <summary>
    /// Regex паттерн для валидации фамилии пользователя.
    /// </summary>
    public string UserLastNamePattern { get; set; }

    /// <summary>
    /// Regex паттерн для валидации пароля пользователя.
    /// </summary>
    public string UserPasswordPattern { get; set; }
}
