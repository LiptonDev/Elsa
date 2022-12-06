using Elsa.Core.Models.Response;
using System.Net;
using System.Text.Json.Serialization;

namespace Elsa.API.Application.Common.Models;

/// <summary>
/// Указывает, что ответ содержит в себе статус запроса.
/// </summary>
public interface IServiceResult : IElsaResult
{
    /// <summary>
    /// Status code.
    /// </summary>
    HttpStatusCode StatusCode { get; }
}

/// <inheritdoc cref="ElsaResult{TResponse}"/>
public class ServiceResult<TResponse> : ElsaResult<TResponse>, IServiceResult
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
    public ServiceResult(TResponse data, ElsaError error, HttpStatusCode statusCode = HttpStatusCode.OK) : base(data, error)
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
    /// Конструктор.
    /// </summary>
    /// <param name="error"></param>
    public ServiceResult(ElsaError error) : base(error)
    {

    }

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
    public ServiceResult()
    {

    }
}
