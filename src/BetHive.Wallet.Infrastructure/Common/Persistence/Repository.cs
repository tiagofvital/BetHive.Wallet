using BetHive.Wallet.Application.Common.Interfaces;

namespace BetHive.Wallet.Infrastructure.Common.Persistence
{
    public class Repository
    {
        protected readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => this.dbContext;
    }
}
