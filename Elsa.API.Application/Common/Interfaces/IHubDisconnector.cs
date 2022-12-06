namespace Elsa.API.Application.Common.Interfaces;

/// <summary>
/// Отключает пользователей от хаба.
/// </summary>
public interface IHubDisconnector
{
    /// <summary>
    /// Отключить пользователя по токену подключения.
    /// </summary>
    /// <param name="token">Токен.</param>
    /// <returns></returns>
    Task DisconnectAsync(string token);
}
