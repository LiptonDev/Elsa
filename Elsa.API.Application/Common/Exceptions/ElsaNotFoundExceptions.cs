namespace Elsa.API.Application.Common.Exceptions;

/// <summary>
/// Объект не найден.
/// </summary>
public class ElsaNotFoundExceptions : Exception
{
    /// <summary>
    /// Конструктор.
    /// </summary>
    public ElsaNotFoundExceptions(object key)
    {
        Key = key;
    }

    /// <summary>
    /// Ключ, по которому производился поиск.
    /// </summary>
    public object Key { get; }
}
