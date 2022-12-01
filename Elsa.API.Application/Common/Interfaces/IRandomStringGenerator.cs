namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис генерации рандомной строки указанной длины.
/// </summary>
public interface IRandomStringGenerator
{
    /// <summary>
    /// Генерация рандомной строки указанной длины.
    /// </summary>
    /// <param name="length">Длина строки.</param>
    /// <returns></returns>
    string Generate(int length);
}
