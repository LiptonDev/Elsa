using Elsa.API.Domain.Settings;

namespace Elsa.API.Extensions;

public static class ConfigureExtensions
{
    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LanguageSettings>(configuration.GetSection("LanguageSettings"));
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.Configure<RegexSettings>(configuration.GetSection("RegexSettings"));
        services.Configure<List<ElsaLanguage>>(configuration.GetSection("Languages"));
    }
}
