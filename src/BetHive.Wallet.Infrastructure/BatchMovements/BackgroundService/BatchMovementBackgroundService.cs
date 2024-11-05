using BetHive.Wallet.Domain.BatchMovements;
using BetHive.Wallet.Domain.Wallets;
using BetHive.Wallet.Infrastructure.Common;

using ErrorOr;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BetHive.Wallet.Infrastructure.BatchMovements.BackgroundService
{
    /*
     * A simplistic implementation of a background job.
     */
    public class BatchMovementBackgroundService(IServiceScopeFactory serviceScopeFactory) : IHostedService
    {
        private readonly AppDbContext _dbContext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        private Timer _timer = null!;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ProcessBatchMovements, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void ProcessBatchMovements(object? state)
        {
            // pause timer
            if (_timer != null)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            try
            {
                var batchMovements = this._dbContext.BatchMovements.Where(i => i.Status == Status.NotStarted).ToList();

                foreach (var batchMovement in batchMovements)
                {
                    Process(batchMovement);
                }
            }
            catch (Exception)
            {
                // log it
            }
            finally
            {
                // resume timer
                if (_timer != null)
                {
                    _timer.Change((int)TimeSpan.FromMinutes(1).TotalMilliseconds, (int)TimeSpan.FromMinutes(1).TotalMilliseconds);
                }
            }
        }

        private void Process(BatchMovement batchMovement)
        {
            var userIds = batchMovement.MovementRequests.Select(i => i.UserId).ToList();

            var wallets = this._dbContext.Wallets.Where(i => i.TenantId == batchMovement.TenantId && userIds.Any(id => id == i.UserId)).ToList();

            foreach (var mvt in batchMovement.MovementRequests)
            {
                var wallet = wallets.FirstOrDefault(i => i.UserId == mvt.UserId);

                if (wallet == null)
                {
                    batchMovement.SetMovementRequestStatus(mvt, Status.RanWithError);
                    mvt.Set(WalletErrors.NotFound.Description);
                    continue;
                }

                ExecuteMovementToWallet(batchMovement, mvt, wallet);
            }

            // save batch
            this._dbContext.SaveChanges();
        }

        private void ExecuteMovementToWallet(BatchMovement batchMovement, MovementRequest mvt, Domain.Wallets.Wallet wallet)
        {
            ErrorOr<Domain.Wallets.Wallet> result = UpdateWallet(mvt, wallet);

            if (result.IsError)
            {
                batchMovement.SetMovementRequestStatus(mvt, Status.RanWithError);
                mvt.Set(result.FirstError.Description);
            }
            else
            {
                batchMovement.SetMovementRequestStatus(mvt, Status.RanWithSucess);
            }
        }

        private static ErrorOr<Domain.Wallets.Wallet> UpdateWallet(MovementRequest mvt, Domain.Wallets.Wallet wallet)
        {
            ErrorOr<Domain.Wallets.Wallet> result;

            if (mvt.OperationType == MovementOperationType.Deposit)
            {
                result = wallet.Deposit(mvt.Amount);
            }
            else
            {
                result = wallet.Withdraw(wallet.Token, mvt.Amount);
            }

            return result;
        }
    }
}
