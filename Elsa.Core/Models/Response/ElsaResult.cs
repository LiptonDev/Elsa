namespace Elsa.Core.Models.Response;

/// <summary>
/// Результат запроса к API.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IElsaResult<TResponse> : IElsaResult
{
    /// <summary>
    /// Ответ.
    /// </summary>
    TResponse Data { get; set; }
}

/// <summary>
/// Результат запроса к API.
/// </summary>
public interface IElsaResult
{
    /// <summary>
    /// Ошибка.
    /// </summary>
    ElsaError Error { get; set; }
}

/// <summary>
/// Стандартный ответ на обращение к сервису.
/// </summary>
public class ElsaResult<TResponse> : IElsaResult<TResponse>
{
    /// <summary>
    /// Ответ.
    /// </summary>
    public TResponse Data { get; set; }

    /// <summary>
    /// Ошибка.
    /// </summary>
    public ElsaError Error { get; set; }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaResult(TResponse data)
    {
        Data = data;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaResult(TResponse data, ElsaError error) : this(error)
    {
        Data = data;
    }


    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaResult(ElsaError error)
    {
        Error = error;
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaResult()
    {

    }
}