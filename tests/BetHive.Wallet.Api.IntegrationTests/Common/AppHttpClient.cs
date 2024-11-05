using System.Net;

using BetHive.Wallet.Contracts.Wallets;
using BetHive.Wallet.Contracts.Wallets.Batch;

namespace BetHive.Wallet.Api.IntegrationTests.Common
{
    public class AppHttpClient(HttpClient _httpClient)
    {
        public async Task<HttpResponseMessage> CreateBatchRequestAsync(CreateBatchMovementRequest request)
        {
           return await _httpClient.PostAsJsonAsync(
               $"api/v1/tenants/{Constants.Wallet.TenantId}/batch/movements",
               request,
               CancellationToken.None);
        }

        public async Task<WalletResult> CreateWalletAndExpectSuccessAsync()
        {
            var request = new CreateWalletRequest(Constants.Wallet.UserId);

            var response = await CreateWalletAsync(request, Constants.Wallet.TenantId);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var walletResponse = await response.Content.ReadFromJsonAsync<WalletResult>();

            walletResponse.Should().NotBeNull();

            return walletResponse!;
        }

        public async Task<HttpResponseMessage> CreateWalletAsync(CreateWalletRequest request, int tenantId)
        {
            return await _httpClient.PostAsJsonAsync(
                $"api/v1/tenants/{tenantId}/wallets",
                request,
                CancellationToken.None);
        }
    }
}