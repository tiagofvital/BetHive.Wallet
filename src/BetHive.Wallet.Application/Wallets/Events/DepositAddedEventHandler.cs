using BetHive.Wallet.Domain.Wallets.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace BetHive.Wallet.Application.Wallets.Events
{
    public class DepositAddedEventHandler : INotificationHandler<DepositAddedEvent>
    {
        private readonly ILogger<DepositAddedEventHandler> _logger;

        public DepositAddedEventHandler(ILogger<DepositAddedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DepositAddedEvent notification, CancellationToken cancellationToken)
        {
            // A Sample handler
            // i.e.: an outbox handler mapping domain events into integration events.
            _logger.LogInformation("Deposit of {@Amount} made in {@Wallet}", notification.DepositAmount, notification.Wallet.Id);

            return Task.CompletedTask;
        }
    }
}
