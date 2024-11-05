using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Infrastructure.BatchMovements;
using BetHive.Wallet.Infrastructure.BatchMovements.BackgroundService;
using BetHive.Wallet.Infrastructure.Common;
using BetHive.Wallet.Infrastructure.CustomerWallets;
using BetHive.Wallet.Infrastructure.Security;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BetHive.Wallet.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpContextAccessor()
                .AddBackgroundServices(configuration)
                .AddAuthorization()
                .AddPersistence(configuration);

            return services;
        }

        private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            BackgroundServiceSettings bckSettings = new();
            configuration.Bind(BackgroundServiceSettings.Section, bckSettings);

            if (!bckSettings.Enable)
            {
                return services;
            }

            services.AddHostedService<BatchMovementBackgroundService>();

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.CommandTimeout(5);
                        sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 1,
                        maxRetryDelay: TimeSpan.FromMilliseconds(10),
                        errorNumbersToAdd: null);
                    }));

            services.AddScoped<IWalletsRepository, WalletsRepository>();
            services.AddScoped<IBatchMovementsRepository, BatchMovementsRepository>();

            return services;
        }

        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}