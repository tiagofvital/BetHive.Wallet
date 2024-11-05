using BetHive.Wallet.Domain.Common;

namespace BetHive.Wallet.Domain.BatchMovements
{
    /// <summary>
    /// TODO: Add Retries Counter.
    /// </summary>
    public class BatchMovement : AuditableEntity
    {
        private readonly List<MovementRequest> _movementRequests;

        public BatchMovement(int tenantId, Guid externalId)
            : this(tenantId, Guid.NewGuid(), externalId, new List<MovementRequest>())
        {
            this.CreatedAt = DateTime.UtcNow;
            this.ModifiedAt = DateTime.UtcNow;
        }

        public BatchMovement(int tenantId, Guid id, Guid externalId, IEnumerable<MovementRequest> movementRequests)
            : base(id)
        {
            this.TenantId = tenantId;
            this.ExternalId = externalId;
            this._movementRequests = movementRequests.ToList();
        }

        public int TenantId { get; init; }

        public Guid ExternalId { get; init; }

        public IReadOnlyCollection<MovementRequest> MovementRequests { get { return _movementRequests.AsReadOnly(); } }

        public Status Status { get; private set; }

        public void AddMovement(Guid userId, MovementOperationType movementType, float amount)
        {
            var mvt = MovementRequest.Create(this.Id, userId, movementType, amount);
            this._movementRequests.Add(mvt);
        }

        public void SetMovementRequestStatus(MovementRequest request, Status status)
        {
            request.Set(status);

            if (this.MovementRequests.All(i => i.Status == Status.RanWithSucess))
            {
                this.Status = Status.RanWithSucess;
            }
            else if (this.MovementRequests.Any(i => i.Status == Status.NotStarted))
            {
                this.Status = Status.Running;
            }
            else if (this.MovementRequests.Any(i => i.Status == Status.RanWithError))
            {
                this.Status = Status.RanWithError;
            }

            this.ModifiedAt = DateTime.UtcNow;
        }
    }
}
