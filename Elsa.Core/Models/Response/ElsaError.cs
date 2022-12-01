using Elsa.Core.Enums;
using Elsa.Core.Models.Errors;

namespace Elsa.Core.Models.Response;

/// <summary>
/// Ошибка сервиса.
/// </summary>
public class ElsaError
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaError(string message, ErrorCode code)
    {
        ErrorCode = code;
        Message = message;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaError(string message, ErrorCode code, ElsaValidationErrors validationErrors) : this(message, code)
    {
        ValidationErrors = validationErrors;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaError()
    {

    }

    /// <summary>
    /// Сообщение ошибки.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Код ошибки.
    /// </summary>
    public ErrorCode ErrorCode { get; set; }

    /// <summary>
    /// Ошибки валидации.
    /// </summary>
    public ElsaValidationErrors ValidationErrors { get; set; }
}