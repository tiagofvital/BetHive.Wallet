using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Infrastructure.Common;
using BetHive.Wallet.Infrastructure.Common.Persistence;

using Microsoft.EntityFrameworkCore;

namespace BetHive.Wallet.Infrastructure.CustomerWallets
{
    public class WalletsRepository : Repository, IWalletsRepository
    {
        public WalletsRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task AddAsync(Domain.Wallets.Wallet wallet, CancellationToken cancellationToken)
        {
            await dbContext.Wallets.AddAsync(wallet, cancellationToken);
        }

        public async Task<IEnumerable<Domain.Wallets.Wallet>> GetAsync(int tenantId, Guid? userId, CancellationToken cancellationToken)
        {
            return await dbContext.Wallets
                .Where(w => w.TenantId == tenantId)
                .Where(w => !userId.HasValue || w.UserId == userId.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<Domain.Wallets.Wallet?> GetByIdAsync(Guid id, int tenantId, CancellationToken cancellationToken)
        {
            return await dbContext.Wallets
                .Where(w => w.Id == id && w.TenantId == tenantId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
