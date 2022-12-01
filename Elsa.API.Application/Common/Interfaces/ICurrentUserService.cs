namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Сервис получения текущего пользователя.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Роли пользователя.
    /// </summary>
    string?[] Roles { get; }

    /// <summary>
    /// Текущий ID пользователя.
    /// </summary>
    string? UserId { get; }
}
