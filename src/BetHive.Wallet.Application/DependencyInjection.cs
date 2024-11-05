using BetHive.Wallet.Application.Common.Behaviors;

using FluentValidation;

using Mapster;

using Microsoft.Extensions.DependencyInjection;

namespace BetHive.Wallet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
                options.AddOpenBehavior(typeof(ResilienceBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
            services.AddMapster();

            return services;
        }
    }
}