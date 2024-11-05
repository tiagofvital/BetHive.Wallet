using BetHive.Wallet.Domain.BatchMovements;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetHive.Wallet.Infrastructure.BatchMovements.Persistence
{
    public class BatchMovementsConfigurations : IEntityTypeConfiguration<BatchMovement>
    {
        public void Configure(EntityTypeBuilder<BatchMovement> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.TenantId);
            builder.Property(w => w.ExternalId);
            builder.OwnsMany(w => w.MovementRequests, a =>
            {
                a.WithOwner().HasForeignKey(m => m.BatchId);
                a.Property(m => m.Id);
                a.Property(m => m.UserId).IsRequired();
                a.Property(m => m.Amount).IsRequired();
                a.Property(m => m.OperationType).HasConversion<int>();
                a.Property(m => m.Status).HasConversion<int>();
                a.Property(m => m.ErrorDescription);
                a.HasKey(m => m.Id);
            });

            builder.Property(m => m.Status).HasConversion<int>();

            builder.Property(w => w.CreatedAt);
            builder.Property(w => w.ModifiedAt);

            builder.HasIndex(w => w.ExternalId).IsUnique();
        }
    }
}
