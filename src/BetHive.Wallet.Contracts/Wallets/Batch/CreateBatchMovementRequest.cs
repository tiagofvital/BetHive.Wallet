namespace BetHive.Wallet.Contracts.Wallets.Batch
{
    public record CreateBatchMovementRequest(Guid ExternalId, IEnumerable<MovementLine> Movements);
}
