namespace BetHive.Wallet.Application.Wallets.Commands.CreateWallet
{
    public record WalletCreatedResult(Guid Id, int TenantId, Guid UserId);
}
