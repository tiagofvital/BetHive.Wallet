using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Domain.BatchMovements;
using BetHive.Wallet.Domain.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BetHive.Wallet.Infrastructure.Common
{
    public class AppDbContext(DbContextOptions options, IPublisher _publisher)
        : DbContext(options), IUnitOfWork
    {
        public DbSet<BatchMovement> BatchMovements { get; set; } = null!;
        public DbSet<Domain.Wallets.Wallet> Wallets { get; set; } = null!;

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
               .SelectMany(entry => entry.Entity.PopDomainEvents())
               .ToList();

            await PublishDomainEvents(domainEvents);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}