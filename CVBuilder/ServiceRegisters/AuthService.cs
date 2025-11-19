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
        services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}
