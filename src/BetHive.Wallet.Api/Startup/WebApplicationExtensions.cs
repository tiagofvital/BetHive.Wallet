using Microsoft.EntityFrameworkCore;

namespace BetHive.Wallet.Api.Startup;

public static class WebApplicationExtensions
{
    public static WebApplication MigrateDatabase<T>(this WebApplication webApplication)
        where T : DbContext
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }

        return webApplication;
    }
}
