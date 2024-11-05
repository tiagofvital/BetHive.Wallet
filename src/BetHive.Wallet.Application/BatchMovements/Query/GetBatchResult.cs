using BetHive.Wallet.Domain.BatchMovements;

namespace BetHive.Wallet.Application.BatchMovements.Query
{
    public record GetBatchResult(Guid Id, int TenantId, Status Status, IEnumerable<MovementRequest> MovementsRequests);
}
