namespace Elsa.API.Extensions;

public static class ConfigFilesExtensions
{
    /// <summary>
    /// Добавить JSON файлы настроек.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddJsonConfigFiles(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("fluentvalidation_translate.json", true, false);
        builder.Configuration.AddJsonFile("resetpasswordtoken_mail_body.json", true, true);
    }
}
