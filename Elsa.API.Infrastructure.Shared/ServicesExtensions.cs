using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.API.Infrastructure.Shared;

public static class ServicesExtensions
{
    /// <summary>
    /// Добавить общие сервисы.
    /// </summary>
    /// <param name="services"></param>
    public static void AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddSingleton<IRandomStringGenerator, RandomStringGenerator>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
    }
}
