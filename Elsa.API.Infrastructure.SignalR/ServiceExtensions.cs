using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.API.Infrastructure.SignalR;

public static class ServiceExtensions
{
    public static void AddSignalrLayer(this IServiceCollection services)
    {
        services.AddSinglaR();
    }

    public static void MapHubs(this IApplicationBuilder builder)
    {
        builder.MapHub();
    }
}
