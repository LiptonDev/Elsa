using Elsa.API.Domain.Settings;
using Elsa.API.FVLanguageManager;
using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Elsa.API.Extensions;

public static class LanguageExtension
{
    /// <summary>
    /// Использовать локализацию.
    /// </summary>
    /// <param name="services"></param>
    public static void AddElsaLocalization(this IServiceCollection services)
    {
        services.AddLocalization(o => o.ResourcesPath = "Localization");
    }

    /// <summary>
    /// Добавить локализацию FluentValidation, установить стандартный язык для API (если не указывается заголовок Accept-Language).
    /// </summary>
    /// <param name="app"></param>
    public static void AddElsaLocalization(this IApplicationBuilder app)
    {
        var defaultLanguage = app.ApplicationServices.GetService<IOptions<LanguageSettings>>().Value.DefaultLanguage;
        var languages = app.ApplicationServices.GetService<IOptions<List<ElsaLanguage>>>().Value;
        var langArray = languages.Select(x => new CultureInfo(x.Name)).ToArray();

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultLanguage),
            SupportedCultures = langArray,
            SupportedUICultures = langArray,
            ApplyCurrentCultureToResponseHeaders = true
        });

        ValidatorOptions.Global.LanguageManager = new CustomLanguageManager(languages);
    }
}
