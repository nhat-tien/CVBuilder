using CVBuilder.Models;
using Microsoft.AspNetCore.Identity;
using CVBuilder.Data.Seeder;

namespace CVBuilder.Cli;

public static class CliHandler
{
    public static async Task Handle(string[] args, WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        switch (args[0].ToLower())
        {
            case "seed-data":
                await SeedData(services);
                break;
            case "seed-admin":
                await SeedSuperAdminAsync(services);
                break;
            default:
                Console.WriteLine("Unknown Command");
                break;
        }
    }


    public static async Task SeedData(IServiceProvider services)
    {
        try
        {
            await DataSeeder.SeedDataAsync(services);
            Console.WriteLine("✅ Data seeded successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error seeding data: " + ex.Message);
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
