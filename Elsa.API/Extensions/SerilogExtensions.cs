using Serilog;

namespace Elsa.API.Extensions;

public static class SerilogExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        var logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        builder.Logging.AddSerilog(logger);
        builder.Services.AddSingleton(logger);
    }
}
