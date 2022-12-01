using Elsa.API.Application;
using Elsa.API.Application.Common.Interfaces;
using Elsa.API.Extensions;
using Elsa.API.Infrastructure;
using Elsa.API.Infrastructure.Persistence;
using Elsa.API.Infrastructure.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

static class Program
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
            var generator = services.ServiceProvider.GetRequiredService<ITokenGenerator>();

            await ElsaDbContextSeed.SeedDefaultUserAsync(userManager, roleManager, generator);
        }

        ConfigureApp(app);
    }

    static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddElsaLocalization();
        builder.Services.ConfigureSettings(builder.Configuration);
        builder.Services.AddInfrastructureLayer();
        builder.Services.AddApplicationLayer();
        builder.Services.AddSharedInfrastructure();
        builder.Services.AddControllersWithJson();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerExtension();
    }

    static void ConfigureApp(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseSwaggerExtensions();
        app.UseAuth();
        app.AddElsaLocalization();
        app.MapControllers();
        app.Run();
    }
}