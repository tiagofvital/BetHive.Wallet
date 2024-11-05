using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetHive.Wallet.Infrastructure.Users.Persistence
{
    public class WalletConfigurations : IEntityTypeConfiguration<Domain.Wallets.Wallet>
    {
        public void Configure(EntityTypeBuilder<Domain.Wallets.Wallet> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .ValueGeneratedNever();

            builder.Property(w => w.TenantId);

            builder.HasIndex(w => w.TenantId);
            builder.HasIndex(w => w.UserId).IsUnique();

            builder.Property(w => w.Balance);
            builder.Property(w => w.Token).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            builder.Property(w => w.CreatedAt);
            builder.Property(w => w.ModifiedAt);
        }
    }
}