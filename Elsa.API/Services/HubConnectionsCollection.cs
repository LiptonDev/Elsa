using Elsa.API.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Elsa.API.Services;

/// <inheritdoc cref="IHubConnectionsCollection"/>
public class HubConnectionsCollection : IHubConnectionsCollection
{
    /// <summary>
    /// Контексты подключений.
    /// </summary>
    private readonly ConcurrentDictionary<string, List<HubCallerContext>> hubCallerContexts = new ConcurrentDictionary<string, List<HubCallerContext>>();

    /// <inheritdoc cref="IHubConnectionsCollection.AddAsync(string, HubCallerContext)"/>
    public Task AddAsync(string elsaBearerToken, HubCallerContext context)
    {
        if (hubCallerContexts.TryGetValue(elsaBearerToken, out var list))
        {
            list.Add(context);
        }
        else
        {
            hubCallerContexts.TryAdd(elsaBearerToken, new List<HubCallerContext>() { context });
        }
        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IHubDisconnector.DisconnectAsync(string)"/>
    public Task DisconnectAsync(string elsaBearerToken)
    {
        if (hubCallerContexts.TryRemove(elsaBearerToken, out var list))
        {
            list.ForEach(x => x.Abort());
        }

        return Task.CompletedTask;
    }
}