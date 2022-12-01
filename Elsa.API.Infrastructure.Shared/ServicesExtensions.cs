using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.API.Infrastructure.Shared;

public static class ServicesExtensions
{
    public static void AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddSingleton<IRandomStringGenerator, RandomStringGenerator>();
        services.AddSingleton<ITokenGenerator, TokenGenerator>();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
    }
}
