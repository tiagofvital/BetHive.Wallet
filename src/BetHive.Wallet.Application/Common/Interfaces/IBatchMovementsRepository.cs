using BetHive.Wallet.Domain.BatchMovements;

namespace BetHive.Wallet.Application.Common.Interfaces
{
    public interface IBatchMovementsRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task AddAsync(BatchMovement movementsRequest, CancellationToken cancellationToken);
        Task<BatchMovement?> GetAsync(int tenantId, Guid id, CancellationToken cancellationToken);
        Task<BatchMovement?> GetByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
    }
}
