using BetHive.Wallet.Application.Common.Security.Permissions;
using BetHive.Wallet.Application.Common.Security.Policies;
using BetHive.Wallet.Application.Common.Security.Request;
using BetHive.Wallet.Application.Wallets.Common;

using ErrorOr;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Commands.Deposit
{
    [Authorize(Permissions = Permission.Wallet.Deposit, Policies = Policy.SelfOrAdmin)]
    public record DepositCommand(int TenantId, Guid WalletId, float Amount)
        : IRequest<ErrorOr<WalletResult>>;
}
