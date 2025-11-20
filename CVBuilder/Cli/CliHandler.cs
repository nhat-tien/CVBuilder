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
}
