namespace BetHive.Wallet.Api.IntegrationTests.Common.WebApplicationFactory
{
    [CollectionDefinition(CollectionName)]
    public class WebAppFactoryCollection : ICollectionFixture<WebAppFactory>
    {
        public const string CollectionName = "WebAppFactoryCollection";
    }
}