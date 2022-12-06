using Elsa.API.Application;
using Elsa.API.Application.Common.Models;
using Elsa.API.Filters;
using Elsa.Core.Enums;
using Elsa.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elsa.API.Extensions;

public static class ServiceExtensions
{
    static readonly ServiceResult<object> modelInvalid = new(new ElsaError("invalid request", ErrorCode.Validation));
    /// <summary>
    /// Controllers.
    /// </summary>
    /// <param name="services"></param>
    public static void AddControllersWithJson(this IServiceCollection services)
    {
        services.AddControllers(x =>
        {
            x.Filters.Add(new ProducesAttribute("application/json"));
            x.Filters.Add<ApiExceptionsFilterAttribute>();
        }).AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }).ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressConsumesConstraintForFormFileParameters = true;
            options.SuppressInferBindingSourcesForParameters = true;
            options.SuppressMapClientErrors = true;
            options.SuppressModelStateInvalidFilter = false;
            options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(modelInvalid);
        });
    }

    /// <summary>
    /// Swagger
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Elsa.API",
                Description = "Hello, world!",
                Contact = new OpenApiContact
                {
                    Name = "Alexander Smirnov",
                    Email = "sash.smirnow2015@yandex.ru",
                    Url = new Uri("https://t.me/ow_dafuq")
                }
            });
            x.AddSecurityDefinition(ElsaSchemeConsts.SchemeName, new OpenApiSecurityScheme
            {
                Name = ElsaSchemeConsts.SchemeBearer,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = ElsaSchemeConsts.SchemeName,
                BearerFormat = ElsaSchemeConsts.SchemeName,
                Description = $"Input your Bearer token in this format - {{your token here}} to access this API"
            });
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = ElsaSchemeConsts.SchemeName,
                        },
                        Scheme = ElsaSchemeConsts.SchemeName,
                        Name = ElsaSchemeConsts.SchemeName,
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            x.IncludeXmlComments(xmlPath, true);
        });
    }
}
