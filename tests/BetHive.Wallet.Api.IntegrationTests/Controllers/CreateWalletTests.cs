using BetHive.Wallet.Api.IntegrationTests.Common;
using BetHive.Wallet.Api.IntegrationTests.Common.WebApplicationFactory;
using BetHive.Wallet.Application.Wallets.Common;

namespace BetHive.Wallet.Api.IntegrationTests.Controllers
{
    [Collection(WebAppFactoryCollection.CollectionName)]

    public class CreateWalletTests
    {
        private readonly AppHttpClient _client;

        public CreateWalletTests(WebAppFactory webAppFactory)
        {
            _client = webAppFactory.CreateAppHttpClient();
            webAppFactory.ResetDatabase();
        }

        [Fact]
        public async Task CreateWallet_WhenUserHasNoWallet_ShouldCreateWallet()
        {
            // Arrange
            var expected = new WalletResult(
                Guid.NewGuid(),
                Constants.Wallet.TenantId,
                Constants.Wallet.UserId,
                Array.Empty<byte>(),
                0);

            // Act
            var response = await _client.CreateWalletAndExpectSuccessAsync();

            // Assert
            response.Should().BeEquivalentTo(expected, opts => opts
                .Excluding(p => p.Id)
                .Excluding(p => p.Token));
        }
    }
}
