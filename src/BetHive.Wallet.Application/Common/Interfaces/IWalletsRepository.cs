namespace BetHive.Wallet.Application.Common.Interfaces
{
    public interface IWalletsRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task AddAsync(Domain.Wallets.Wallet wallet, CancellationToken cancellationToken);
        Task<IEnumerable<Domain.Wallets.Wallet>> GetAsync(int tenantId, Guid? userId, CancellationToken cancellationToken);
        Task<Domain.Wallets.Wallet?> GetByIdAsync(Guid id, int tenantId, CancellationToken cancellationToken);
    }
}
