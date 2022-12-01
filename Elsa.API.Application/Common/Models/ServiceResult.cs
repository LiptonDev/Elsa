using Elsa.Core.Models.Response;
using System.Net;
using System.Text.Json.Serialization;

namespace Elsa.API.Application.Common.Models;

/// <summary>
/// Указывает, что ответ содержит в себе статус запроса.
/// </summary>
public interface IStatus
{
    /// <summary>
    /// Status code.
    /// </summary>
    HttpStatusCode StatusCode { get; }
}

/// <inheritdoc cref="ElsaResult{TResponse}"/>
public class ServiceResult<TResponse> : ElsaResult<TResponse>, IStatus
{
    /// <summary>
    /// Status code.
    /// </summary>
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ServiceResult(TResponse data, HttpStatusCode statusCode = HttpStatusCode.OK) : this(statusCode)
    {
        Data = data;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ServiceResult(TResponse data, ElsaError error, HttpStatusCode statusCode = HttpStatusCode.OK) : base(error)
    {
        Data = data;
        StatusCode = statusCode;
    }


    /// <summary>
    /// Конструктор.
    /// </summary>
    public ServiceResult(ElsaError error, HttpStatusCode statusCode = HttpStatusCode.OK) : base(error)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ServiceResult(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        StatusCode = statusCode;
    }
}

/// <inheritdoc cref="ElsaResult"/>
public class ServiceResult : ElsaResult, IStatus
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ServiceResult(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ServiceResult(ElsaError error, HttpStatusCode statusCode = HttpStatusCode.OK) : base(error)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Status code.
    /// </summary>
    [JsonIgnore]
    public HttpStatusCode StatusCode { get; }
}
