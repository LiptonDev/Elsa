using Elsa.API.Domain.Settings;
using Microsoft.Extensions.Options;

namespace Elsa.API.Extensions;

/// <summary>
/// Redis cache.
/// </summary>
public static class RedisCacheExtensions
{
    /// <summary>
    /// Add redis cache.
    /// </summary>
    public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = new RedisSettings();
        configuration.GetSection("RedisSettings").Bind(settings);
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = settings.ConnectionString;
        });
    }
}
