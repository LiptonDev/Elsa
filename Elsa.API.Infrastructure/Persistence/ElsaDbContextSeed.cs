using Elsa.API.Domain.Settings;
using Elsa.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Elsa.API.Infrastructure.Persistence;

public class ElsaDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ElsaUser> userManager, RoleManager<ElsaRole> roleManager, DefaultAdminUser user)
    {
        var adminRole = new ElsaRole(Roles.Admin.ToString());

        var any = await roleManager.Roles.AnyAsync(x => x.Name == adminRole.Name);
        if (!any)
        {
            await roleManager.CreateAsync(adminRole);
        }

        var defaultUser = new ElsaUser
        {
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
        {
            await userManager.CreateAsync(defaultUser, user.Password);
            await userManager.AddToRoleAsync(defaultUser, adminRole.Name!);
        }
    }
}
