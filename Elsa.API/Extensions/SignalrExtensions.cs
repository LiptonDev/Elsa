using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Hubs;
using Elsa.API.Services;

namespace Elsa.API.Extensions;

public static class SignalrExtensions
{
    /// <summary>
    /// Добавить SignalR.
    /// </summary>
    /// <param name="services"></param>
    public static void AddSignalrLayer(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<IHubConnectionsCollection, HubConnectionsCollection>();
        services.AddSingleton<IHubDisconnector>(x => x.GetRequiredService<IHubConnectionsCollection>());
    }

    /// <summary>
    /// Map hubs.
    /// </summary>
    /// <param name="application"></param>
    public static void MapHubs(this WebApplication application)
    {
        application.MapHub<TestHub>("/test");
    }
}
