using Elsa.API.Domain.Settings;
using Elsa.API.FVLanguageManager;
using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Elsa.API.Extensions;

public static class LanguageExtension
{
    public static void AddElsaLocalization(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("fvlangs.json", true, false);
        builder.Services.AddLocalization(o => o.ResourcesPath = "Localization");
    }

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
