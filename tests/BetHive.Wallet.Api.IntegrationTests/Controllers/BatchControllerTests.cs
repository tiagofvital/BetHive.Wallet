using System.Net;
using System.Net.Http;

using BetHive.Wallet.Api.IntegrationTests.Common;
using BetHive.Wallet.Api.IntegrationTests.Common.WebApplicationFactory;
using BetHive.Wallet.Application.Wallets.Common;
using BetHive.Wallet.Contracts.Wallets.Batch;

namespace BetHive.Wallet.Api.IntegrationTests.Controllers
{
    [Collection(WebAppFactoryCollection.CollectionName)]
    public class BatchControllerTests
    {
        private readonly AppHttpClient _client;

        public BatchControllerTests(WebAppFactory webAppFactory)
        {
            _client = webAppFactory.CreateAppHttpClient();
            webAppFactory.ResetDatabase();
        }

        [Fact]
        public async Task CreateBatchMovements_WhenValidBatch_ShouldBeAccepted()
        {
            // Arrange
            var expected = new WalletResult(Guid.NewGuid(), Constants.Wallet.TenantId, Constants.Wallet.UserId, Array.Empty<byte>(), 0);

            var movements = new List<MovementLine> { new MovementLine(Constants.Wallet.UserId, WalletMovementType.Deposit, 10) };

            var batchRequest = new CreateBatchMovementRequest(ExternalId: Guid.NewGuid(), movements);

            // Act
            var response = await _client.CreateBatchRequestAsync(batchRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        }

        [Fact]
        public async Task CreateBatchMovements_WhenDuplicatedBatch_ShouldBeConflict()
        {
            // Arrange
            var expected = new WalletResult(Guid.NewGuid(), Constants.Wallet.TenantId, Constants.Wallet.UserId, Array.Empty<byte>(), 0);

            var movements = new List<MovementLine> { new MovementLine(Constants.Wallet.UserId, WalletMovementType.Deposit, 10) };

            var batchRequest = new CreateBatchMovementRequest(ExternalId: Guid.NewGuid(), movements);

            // Act
            _ = await _client.CreateBatchRequestAsync(batchRequest);
            var response = await _client.CreateBatchRequestAsync(batchRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}