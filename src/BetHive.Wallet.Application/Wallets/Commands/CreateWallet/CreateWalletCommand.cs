using BetHive.Wallet.Application.Common.Security.Permissions;
using BetHive.Wallet.Application.Common.Security.Policies;
using BetHive.Wallet.Application.Common.Security.Request;

using ErrorOr;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Commands.CreateWallet
{
    [Authorize(Permissions = Permission.Wallet.Create, Policies = Policy.SelfOrAdmin)]
    public record CreateWalletCommand(int TenantId, Guid UserId)
        : IRequest<ErrorOr<WalletCreatedResult>>;
}