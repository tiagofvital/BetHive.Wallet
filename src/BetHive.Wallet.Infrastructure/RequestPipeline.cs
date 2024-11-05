using BetHive.Wallet.Infrastructure.Common;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetHive.Wallet.Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<AppDbContext>();

                    // in a real-world scenario this should be executed in the pipeline
                    // and not during the app startup!!
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<IApplicationBuilder>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }

                return app;
            }
        }
    }
}