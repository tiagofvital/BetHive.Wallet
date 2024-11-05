using BetHive.Wallet.Domain.Common;

namespace BetHive.Wallet.Domain.Wallets.Events;

public record DepositAddedEvent(Wallet Wallet, float DepositAmount) : IDomainEvent;

