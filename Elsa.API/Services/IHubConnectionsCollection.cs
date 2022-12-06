using Elsa.API.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Elsa.API.Services;

/// <summary>
/// Список подключений к хабам.
/// </summary>
public interface IHubConnectionsCollection : IHubDisconnector
{
    /// <summary>
    /// Добавить подключение.
    /// </summary>
    /// <param name="elsaBearerToken">Токен, по которому подключен пользователь.</param>
    /// <param name="context">Контекст подключения.</param>
    /// <returns></returns>
    Task AddAsync(string elsaBearerToken, HubCallerContext context);
}
