namespace BetHive.Wallet.Contracts.Wallets
{
    public record WalletResult(Guid Id, int TenantId, Guid UserId, byte[] Token, float Balance);
}
