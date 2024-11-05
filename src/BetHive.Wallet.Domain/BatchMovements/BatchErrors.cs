using ErrorOr;

namespace BetHive.Wallet.Domain.BatchMovements
{
    public static class BatchErrors
    {
        public static Error BatchNotFound { get; } = Error.NotFound(
            code: "Batch.NotFound",
            description: "Batch not found.");

        public static Error BatchWithExternalIDDuplicated { get; } = Error.Conflict(
            code: "Batch.ExternalIDDuplicated",
            description: "Batch with same external id already exists.");
    }
}
