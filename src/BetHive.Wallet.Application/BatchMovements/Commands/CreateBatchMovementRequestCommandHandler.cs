using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Domain.BatchMovements;

using ErrorOr;

using Mapster;

using MediatR;

namespace BetHive.Wallet.Application.BatchMovements.Commands
{
    public class CreateBatchMovementRequestCommandHandler : IRequestHandler<CreateBatchMovementRequestCommand, ErrorOr<BatchMovementRequestCreatedResult>>
    {
        private readonly IBatchMovementsRepository _repository;

        public CreateBatchMovementRequestCommandHandler(IBatchMovementsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<BatchMovementRequestCreatedResult>> Handle(CreateBatchMovementRequestCommand request, CancellationToken cancellationToken)
        {
            var duplicatedBatch = await this._repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

            if (duplicatedBatch != null)
            {
                return BatchErrors.BatchWithExternalIDDuplicated;
            }

            var batch = new BatchMovement(request.TenantId, request.ExternalId);

            foreach (var item in request.Movements)
            {
                var mvtType = item.MovementType.Adapt<MovementOperationType>();

                batch.AddMovement(item.UserId, mvtType, item.Amount);
            }

            await this._repository.AddAsync(batch, cancellationToken);

            await this._repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var result = new BatchMovementRequestCreatedResult(batch.Id, batch.ExternalId);

            return result;
        }
    }
}
