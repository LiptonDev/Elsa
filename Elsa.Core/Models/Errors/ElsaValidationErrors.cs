namespace Elsa.Core.Models.Errors;

/// <summary>
/// Ошибки валидации.
/// </summary>
public class ElsaValidationErrors
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaValidationErrors(IEnumerable<IGrouping<string, string>> errors)
    {
        Errors = new List<ElsaValidationError>(errors.Select(x => new ElsaValidationError(x.Key, x.ToArray())));
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaValidationErrors()
    {

    }

    /// <summary>
    /// Ошибки валидации.
    /// </summary>
    public List<ElsaValidationError> Errors { get; set; }
}
