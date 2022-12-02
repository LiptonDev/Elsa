namespace Elsa.Core.Models.Response;

/// <summary>
/// Стандартный ответ на обращение к сервису.
/// </summary>
public class ElsaResult<TResponse> : ElsaResult
{
    /// <summary>
    /// Ответ.
    /// </summary>
    public TResponse Data { get; set; }

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
    public ElsaResult(TResponse data, ElsaError error) : base(error)
    {
        Data = data;
    }


    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaResult(ElsaError error) : base(error)
    {
    }

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaResult()
    {

    }
}

/// <summary>
/// Стандартный ответ на обращение к сервису.
/// </summary>
public class ElsaResult
{
    /// <summary>
    /// Ошибка.
    /// </summary>
    public ElsaError Error { get; set; }

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