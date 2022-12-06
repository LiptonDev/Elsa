namespace Elsa.API.Domain.Settings;

/// <summary>
/// Отвечает за тело сообщения на почту с токеном сброса пароля.
/// </summary>
public class ResetTokenMailBodySettings
{
    /// <summary>
    /// Локализированный текст "Ваш токен сброса".
    /// </summary>
    public const string LocalizeText = "{localize_text}";

    /// <summary>
    /// Токен.
    /// </summary>
    public const string TokenText = "{token_text}";

    /// <summary>
    /// <para>Тело сообщения.</para>
    /// <para>{localize_text} - заменяется на локализированный текст "Ваш токен сброса".</para>
    /// <para>{token_text} - заменяется на токен сброса.</para>
    /// </summary>
    public string Body { get; set; }
}
