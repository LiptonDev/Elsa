namespace Elsa.API.Domain.Settings;

/// <summary>
/// Настройки для почты.
/// </summary>
public class MailSettings
{
    /// <summary>
    /// От кого отправлено письмо.
    /// </summary>
    public string EmailFrom { get; set; }

    /// <summary>
    /// Адрес почтового сервера.
    /// </summary>
    public string SmtpHost { get; set; }

    /// <summary>
    /// Порт почтового сервера.
    /// </summary>
    public int SmtpPort { get; set; }

    /// <summary>
    /// Пользователь почтового сервера.
    /// </summary>
    public string SmtpUser { get; set; }

    /// <summary>
    /// Пароль почтового сервера.
    /// </summary>
    public string SmtpPass { get; set; }

    /// <summary>
    /// Отображаемое имя отправителя.
    /// </summary>
    public string DisplayName { get; set; }
}