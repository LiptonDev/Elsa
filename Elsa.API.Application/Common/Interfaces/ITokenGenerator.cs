namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Генерация токенов авторизации.
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Генерация токена авторизации.
    /// </summary>
    /// <returns></returns>
    Task<string> GenerateTokenAsync();
}
