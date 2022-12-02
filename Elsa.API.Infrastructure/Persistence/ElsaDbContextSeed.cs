using Elsa.API.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Elsa.API.Infrastructure.Persistence;

public class ElsaDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ElsaUser> userManager, RoleManager<ElsaRole> roleManager, ITokenGenerator tokenGenerator)
    {
        var adminRole = new ElsaRole("Admin", "Admin role");

        if (roleManager.Roles.All(r => r.Name != adminRole.Name))
        {
            await roleManager.CreateAsync(adminRole);
        }

        var defaultUser = new ElsaUser
        {
            UserName = "sash.smirnow2015@yandex.ru",
            Email = "sash.smirnow2015@yandex.ru",
            FirstName = "Александр",
            LastName = "Смирнов"
        };
        var keys = new List<ElsaApiKey>
        {
            new ElsaApiKey { Key = await tokenGenerator.GenerateTokenAsync(), User = defaultUser }
        };
        defaultUser.ApiKeys = keys;

        if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
        {
            await userManager.CreateAsync(defaultUser, "123456789Css");
            await userManager.AddToRolesAsync(defaultUser, new[] { adminRole.Name });
        }
    }
}
