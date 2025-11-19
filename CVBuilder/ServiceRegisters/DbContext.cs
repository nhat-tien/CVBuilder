using CVBuilder.Data;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.ServiceRegisters;

public static class DbContext
{

    public static IServiceCollection AddDb(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}

