namespace BetHive.Wallet.Contracts.Wallets.Batch
{
    public record CreateBatchMovementResult(Guid Id, Guid ExternalId, IEnumerable<MovementLineResult> LineResults);
}
