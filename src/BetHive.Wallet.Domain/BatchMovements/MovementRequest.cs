using BetHive.Wallet.Domain.Common;

namespace BetHive.Wallet.Domain.BatchMovements
{
    public class MovementRequest : Entity
    {
        public MovementRequest(
            Guid id,
            Guid batchId,
            Guid userId,
            MovementOperationType operationType,
            float amount,
            Status status,
            string errorDescription)
            : base(id)
        {
            BatchId = batchId;
            UserId = userId;
            OperationType = operationType;
            Amount = amount;
            Status = status;
            ErrorDescription = errorDescription;
        }

        public static MovementRequest Create(
            Guid batchId,
            Guid userId,
            MovementOperationType operationType,
            float amount)
        {
            return new MovementRequest(Guid.NewGuid(), batchId, userId, operationType, amount, Status.NotStarted, string.Empty);
        }

        public Guid BatchId { get; }
        public Guid UserId { get; }
        public MovementOperationType OperationType { get; }
        public float Amount { get; }
        public Status Status { get; private set; }
        public string ErrorDescription { get; private set; }

        public MovementRequest Set(Status status)
        {
            this.Status = status;
            return this;
        }

        public MovementRequest Set(string errorDescription)
        {
            this.ErrorDescription = errorDescription;
            return this;
        }
    }
}