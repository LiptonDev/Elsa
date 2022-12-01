using Elsa.API.Domain.Settings;
using FluentValidation.Resources;

namespace Elsa.API.FVLanguageManager;

/// <summary>
/// Языковой менеджер.
/// </summary>
public class CustomLanguageManager : LanguageManager
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public CustomLanguageManager(IEnumerable<ElsaLanguage> langs)
    {
        langs.ForEach(x => x.Items.ForEach(l => AddTranslation(x.Name, l.Name, l.Text)));
    }
}
