namespace Elsa.API.Domain.Settings;

/// <summary>
/// Поддерживаемый язык.
/// </summary>
public class ElsaLanguage
{
    /// <summary>
    /// Имя языка (сокращенное, например: ru, en).
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Элементы.
    /// </summary>
    public List<ElsaLanguageFVItem> Items { get; set; }
}
