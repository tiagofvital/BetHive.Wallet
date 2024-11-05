using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Domain.BatchMovements;

using ErrorOr;

using MediatR;

namespace BetHive.Wallet.Application.BatchMovements.Query
{
    public class GetBatchByIdQueryHandler : IRequestHandler<GetBatchByIdQuery, ErrorOr<GetBatchResult>>
    {
        private readonly IBatchMovementsRepository _repository;

        public GetBatchByIdQueryHandler(IBatchMovementsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<GetBatchResult>> Handle(GetBatchByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await this._repository.GetAsync(request.TenantId, request.Id, cancellationToken);

            if (result == null)
            {
                return BatchErrors.BatchNotFound;
            }

            return new GetBatchResult(result.Id, result.TenantId, result.Status, result.MovementRequests);
        }
    }
}
