namespace Elsa.Core.Models.Errors;

/// <summary>
/// Ошибка валидации.
/// </summary>
public class ElsaValidationError
{
    /// <summary>
    /// Название свойства.
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// Ошибки.
    /// </summary>
    public string[] Errors { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaValidationError(string propertyName, string[] errors)
    {
        PropertyName = propertyName;
        Errors = errors;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaValidationError()
    {

    }
}
