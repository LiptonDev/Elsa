using Elsa.API.Application;
using Elsa.API.Domain.Settings;
using Elsa.API.Extensions;
using Elsa.API.Infrastructure;
using Elsa.API.Infrastructure.Email;
using Elsa.API.Infrastructure.Persistence;
using Elsa.API.Infrastructure.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder);

        var app = builder.Build();

        using (var services = app.Services.CreateScope())
        {
            var context = services.ServiceProvider.GetRequiredService<ElsaDbContext>();

            await context.Database.MigrateAsync();

            var userManager = services.ServiceProvider.GetRequiredService<UserManager<ElsaUser>>();
            var roleManager = services.ServiceProvider.GetRequiredService<RoleManager<ElsaRole>>();
            var user = services.ServiceProvider.GetRequiredService<IOptions<DefaultAdminUser>>();

            await ElsaDbContextSeed.SeedDefaultUserAsync(userManager, roleManager, user.Value);
        }

        ConfigureApp(app);
    }

    static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddSerilog();
        builder.AddJsonConfigFiles();
        builder.Services.AddElsaLocalization();
        builder.Services.ConfigureSettings(builder.Configuration);
        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer();
        builder.Services.AddSharedInfrastructure();
        builder.Services.AddEmailInfrastructure();
        builder.Services.AddQuartzJobs();
        builder.Services.AddControllersWithJson();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerExtension();
        builder.Services.AddRedisCache(builder.Configuration);
        builder.Services.AddSignalrLayer();
    }

    static void ConfigureApp(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseSwaggerExtensions();
        app.UseAuth();
        app.AddElsaLocalization();
        app.MapControllers();
        app.MapHubs();

        app.Run();
    }
}