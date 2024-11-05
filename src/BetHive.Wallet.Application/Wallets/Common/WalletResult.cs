namespace BetHive.Wallet.Application.Wallets.Common
{
    public record WalletResult(Guid Id, int TenantId, Guid UserId, byte[] Token, float Balance);
}
