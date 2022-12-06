using Elsa.API.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Elsa.API.Infrastructure.SignalR.Services;

/// <inheritdoc cref="IHubConnectionsCollection"/>
public class HubConnectionsCollection : IHubConnectionsCollection
{
    /// <summary>
    /// Контексты подключений.
    /// </summary>
    ConcurrentDictionary<string, HubCallerContext> hubCallerContexts = new ConcurrentDictionary<string, HubCallerContext>();

    public Task AddAsync(string elsaBearerToken, HubCallerContext context)
    {
        hubCallerContexts.TryAdd(elsaBearerToken, context);
        return Task.CompletedTask;
    }

    public Task DisconnectAsync(string elsaBearerToken)
    {
        if (hubCallerContexts.ContainsKey(elsaBearerToken))
        {
            hubCallerContexts.Remove(elsaBearerToken, out var context);
            context.Abort();
        }

        return Task.CompletedTask;
    }
}
