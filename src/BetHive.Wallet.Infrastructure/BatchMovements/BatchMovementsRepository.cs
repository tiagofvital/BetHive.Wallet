using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Domain.BatchMovements;
using BetHive.Wallet.Infrastructure.Common;
using BetHive.Wallet.Infrastructure.Common.Persistence;

using Microsoft.EntityFrameworkCore;

namespace BetHive.Wallet.Infrastructure.BatchMovements
{
    public class BatchMovementsRepository : Repository, IBatchMovementsRepository
    {
        public BatchMovementsRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task AddAsync(BatchMovement movementsRequest, CancellationToken cancellationToken)
        {
            await this.dbContext.BatchMovements.AddAsync(movementsRequest, cancellationToken);
        }

        public async Task<BatchMovement?> GetAsync(int tenantId, Guid id, CancellationToken cancellationToken)
        {
            return await this.dbContext.BatchMovements.FirstOrDefaultAsync(b => b.TenantId == tenantId && b.Id == id, cancellationToken);
        }

        public async Task<BatchMovement?> GetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        {
            return await this.dbContext.BatchMovements.FirstOrDefaultAsync(b => b.ExternalId == externalId, cancellationToken);
        }
    }
}
