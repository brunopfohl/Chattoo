using Chattoo.Domain.ValueObjects;
using Chattoo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Chattoo.Infrastructure.Persistence
{
    /// <summary>
    /// Třída obsahující inicializaci výchozího stavu databáze.
    /// </summary>
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "kHoLtNBYkBHIlO@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, @"te6ZbZF_s22Z26sLsn$H2#2pv@H7Z+");
                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

#pragma warning disable 1998
        public static async Task SeedDefaultDataAsync(ApplicationDbContext context)
#pragma warning restore 1998
        {
        }
    }
}
