using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Application.Wallets.Common;
using BetHive.Wallet.Domain.Wallets;

using ErrorOr;

using Mapster;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Commands.Deposit
{
    public class DepositCommandHandler
        : IRequestHandler<DepositCommand, ErrorOr<WalletResult>>
    {
        private readonly IWalletsRepository _repository;

        public DepositCommandHandler(IWalletsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<WalletResult>> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            // load wallet
            var wallet = await _repository.GetByIdAsync(request.WalletId, request.TenantId, cancellationToken);

            if (wallet == null)
            {
                return WalletErrors.NotFound;
            }

            // add amounts
            var addFundResult = wallet.Deposit(request.Amount);

            if (addFundResult.IsError)
            {
                return addFundResult.Errors;
            }

            await this._repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return wallet.Adapt<WalletResult>();
        }
    }
}