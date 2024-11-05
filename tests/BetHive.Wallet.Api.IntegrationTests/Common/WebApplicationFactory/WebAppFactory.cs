using BetHive.Wallet.Infrastructure.Common;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Testcontainers.MsSql;

namespace BetHive.Wallet.Api.IntegrationTests.Common.WebApplicationFactory
{
    public class WebAppFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
    {
        private readonly MsSqlContainer _container = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("BetHive2024!")
            .Build();

        public AppHttpClient CreateAppHttpClient()
        {
            return new AppHttpClient(CreateClient());
        }

        public Task InitializeAsync()
        {
            return _container.StartAsync();
        }

        public new Task DisposeAsync()
        {
            return _container.StopAsync();
        }

        public void ResetDatabase()
        {
            return;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => services
                .RemoveAll<DbContextOptions<AppDbContext>>()
                .AddDbContext<AppDbContext>((sp, options) => options.UseSqlServer(_container.GetConnectionString())));

            builder.ConfigureAppConfiguration((context, conf) => conf.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "BackgroundJobSettings:Enable", "false" },
            }));
        }
    }
}