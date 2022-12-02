using Elsa.Core.Enums;
using System.Net;

namespace Elsa.API.Application.Common.Exceptions;

/// <summary>
/// Ошибка API.
/// </summary>
public class ElsaApiException : Exception
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaApiException(string? message, ErrorCode errorCode = ErrorCode.Internal, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }

    /// <summary>
    /// Код ошибки.
    /// </summary>
    public ErrorCode ErrorCode { get; }

    /// <summary>
    /// Status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; }
}
