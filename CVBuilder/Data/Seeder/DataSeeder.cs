using CVBuilder.Models;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Data.Seeder;
public static class DataSeeder
{
    public static async Task SeedDataAsync(IServiceProvider services)
    {
    }
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    public static async Task SeedSuperAdminAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var config = serviceProvider.GetRequiredService<IConfiguration>();

        string superAdminEmail = config["Admin:Email"]!;
        string superAdminPassword = config["Admin:Password"]!;
        string superAdminFullname = config["Admin:FullName"]!;

        var adminUser = await userManager.FindByEmailAsync(superAdminEmail);
        if (adminUser == null)
        {
            adminUser = new User()
            {
                UserName = superAdminEmail,
                Email = superAdminEmail,
                EmailConfirmed = true,
                Name = superAdminFullname,
            };

            var result = await userManager.CreateAsync(adminUser, superAdminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                throw new Exception("Failed to create super admin:\n" +
                    string.Join("\n", result.Errors.Select(e => e.Description)));
            }
        }
    }
}

