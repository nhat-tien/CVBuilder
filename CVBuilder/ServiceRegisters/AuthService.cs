using CVBuilder.Data;
using CVBuilder.Models;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.ServiceRegisters;

public static class AuthService
{

    public static IServiceCollection AddAuthServices(
        this IServiceCollection services
    )
    {
        services.AddIdentity<User, IdentityRole>(options => {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.SignIn.RequireConfirmedAccount = false;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
