using BetHive.Wallet.Application.Common.Security.Permissions;
using BetHive.Wallet.Application.Common.Security.Policies;
using BetHive.Wallet.Application.Common.Security.Request;
using BetHive.Wallet.Application.Wallets.Common;

using ErrorOr;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Queries;

[Authorize(Permissions = Permission.Wallet.Read, Policies = Policy.SelfOrAdmin)]
public record GetWalletsQuery(int TenantId, Guid? UserId) : IRequest<ErrorOr<IEnumerable<WalletResult>>>;
