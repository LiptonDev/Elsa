using FluentValidation.Results;

namespace Elsa.API.Application.Common.Exceptions;

/// <summary>
/// Ошибка валидации.
/// </summary>
public class ElsaValidationException : Exception
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaValidationException(string message, IEnumerable<ValidationFailure> errors) : base(message)
    {
        Errors = errors.GroupBy(x => x.PropertyName, x => x.ErrorMessage);
    }

    /// <summary>
    /// Ошибки.
    /// </summary>
    public IEnumerable<IGrouping<string, string>> Errors { get; private set; }
}
