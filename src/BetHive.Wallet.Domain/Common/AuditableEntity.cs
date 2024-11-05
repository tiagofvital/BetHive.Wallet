namespace BetHive.Wallet.Domain.Common
{
    public class AuditableEntity : Entity
    {
        protected AuditableEntity(Guid id)
            : base(id) { }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
