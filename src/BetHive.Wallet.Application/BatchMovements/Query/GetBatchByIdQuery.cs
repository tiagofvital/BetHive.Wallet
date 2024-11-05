using ErrorOr;

using MediatR;

namespace BetHive.Wallet.Application.BatchMovements.Query
{
    public record GetBatchByIdQuery(Guid Id, int TenantId) : IRequest<ErrorOr<GetBatchResult>>;
}
