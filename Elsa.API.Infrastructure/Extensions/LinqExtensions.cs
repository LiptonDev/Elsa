namespace System.Linq;

/// <summary>
/// Расширения для LINQ.
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// Добавить в последовательность элемент в конец.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">Исходная последовательность.</param>
    /// <param name="element">Элемент.</param>
    /// <returns></returns>
    public static IEnumerable<T> Add<T>(this IEnumerable<T> source, T element)
    {
        return source.Concat(element.CreateEnumerable());
    }

    /// <summary>
    /// Создать последовательность из одного элемента.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="element">Элемент.</param>
    /// <returns></returns>
    public static IEnumerable<T> CreateEnumerable<T>(this T element)
    {
        yield return element;
    }
}
