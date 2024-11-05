using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Contracts.Wallets;
using BetHive.Wallet.Domain.Wallets;

using ErrorOr;

using Mapster;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Commands.Deposit
{
    internal class WithdrawCommandHandler
        : IRequestHandler<WithdrawCommand, ErrorOr<WalletResult>>
    {
        private readonly IWalletsRepository _repository;

        public WithdrawCommandHandler(IWalletsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<WalletResult>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            // load wallet
            var wallet = await _repository.GetByIdAsync(request.WalletId, request.TenantId, cancellationToken);

            if (wallet == null)
            {
                return WalletErrors.NotFound;
            }

            // add amounts
            var withdrawResult = wallet.Withdraw(request.Token, request.Amount);

            if (withdrawResult.IsError)
            {
                return withdrawResult.Errors;
            }

            return wallet.Adapt<WalletResult>();
        }
    }
}