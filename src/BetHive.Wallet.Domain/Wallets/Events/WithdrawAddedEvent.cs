using BetHive.Wallet.Domain.Common;

namespace BetHive.Wallet.Domain.Wallets.Events;

public record WithdrawAddedEvent(Wallet Wallet, float WithdrawAmount) : IDomainEvent;

