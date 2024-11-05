namespace BetHive.Wallet.Contracts.Wallets.Batch
{
    public record MovementLine(Guid UserId, WalletMovementType MovementType, float Amount);
}
