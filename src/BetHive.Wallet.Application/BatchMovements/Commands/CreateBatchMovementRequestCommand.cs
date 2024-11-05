using BetHive.Wallet.Application.Common.Security.Permissions;
using BetHive.Wallet.Application.Common.Security.Policies;
using BetHive.Wallet.Application.Common.Security.Request;
using BetHive.Wallet.Contracts.Wallets.Batch;

using ErrorOr;

using MediatR;

namespace BetHive.Wallet.Application.BatchMovements.Commands
{
    [Authorize(Permissions = Permission.Batch.Create, Policies = Policy.SelfOrAdmin)]
    public record CreateBatchMovementRequestCommand(int TenantId, Guid ExternalId, IEnumerable<MovementLine> Movements)
        : IRequest<ErrorOr<BatchMovementRequestCreatedResult>>;
}
