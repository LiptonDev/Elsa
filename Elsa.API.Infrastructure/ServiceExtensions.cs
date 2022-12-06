using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Infrastructure.Authentication;
using Elsa.API.Infrastructure.Identity;
using Elsa.API.Infrastructure.Persistence;
using Elsa.API.Infrastructure.Repositories;
using Elsa.API.Infrastructure.Services;
using Elsa.API.Infrastructure.Shared.Services;
using Elsa.Core.Enums;
using Elsa.Core.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elsa.API.Infrastructure;

public static class ServiceExtensions
{
    readonly static JsonSerializerOptions jsonoptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    readonly static ElsaResult<object> forbidden = new(new ElsaError("Forbidden", ErrorCode.Unauthorized));
    readonly static ElsaResult<object> unauthorized = new(new ElsaError("Unauthorized", ErrorCode.Unauthorized));

    /// <summary>
    /// Добавить инфраструктурный слой.
    /// </summary>
    /// <param name="services"></param>
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddTransient(typeof(IAsyncRepository<,>), typeof(AsyncRepository<,>));
        services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IDomainEventsService, DomainEventsService>();
        services.AddTransient<ITokenGenerator, TokenGenerator>();

        services.AddDbContext<ElsaDbContext>(x =>
        {
            x.UseSqlite("filename=elsa.db");
            x.EnableSensitiveDataLogging();
        });

        services.AddIdentityCore<ElsaUser>()
                .AddRoles<ElsaRole>()
                .AddErrorDescriber<IdentityLocalization>()
                .AddEntityFrameworkStores<ElsaDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = ElsaSchemeConsts.SchemeName;
            options.DefaultAuthenticateScheme = ElsaSchemeConsts.SchemeName;
            options.DefaultChallengeScheme = ElsaSchemeConsts.SchemeName;
        }).AddScheme<ElsaSchemeOptions, ElsaSchemeHandler>(ElsaSchemeConsts.SchemeName, options =>
        {
            options.Events = new ElsaSchemeEvents
            {
                OnForbidden = context =>
                {
                    context.Response.ContentType = "application/json; charset=utf-8";
                    return JsonSerializer.SerializeAsync(context.Response.Body, forbidden, options: jsonoptions);
                },
                OnChallenge = context =>
                {
                    context.Response.ContentType = "application/json; charset=utf-8";
                    return JsonSerializer.SerializeAsync(context.Response.Body, unauthorized, options: jsonoptions);
                }
            };
        });
    }
}
