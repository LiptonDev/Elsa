namespace Elsa.API.Extensions;

public static class ConfigFilesExtensions
{
    /// <summary>
    /// Добавить JSON файлы настроек.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddJsonConfigFiles(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("fvlangs.json", true, false);
    }
}
