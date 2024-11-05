using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Domain.Wallets;

using ErrorOr;

using Mapster;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Commands.CreateWallet
{
    public class CreateWalletCommandHandler
        : IRequestHandler<CreateWalletCommand, ErrorOr<WalletCreatedResult>>
    {
        private readonly IWalletsRepository _repository;

        public CreateWalletCommandHandler(IWalletsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<WalletCreatedResult>> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            // check if user already has a customer wallet
            var wallets = await _repository.GetAsync(request.TenantId, request.UserId, cancellationToken);

            if (wallets.Any())
            {
                return WalletErrors.CannotCreateWalletWhenUserAlreadyHasOne;
            }

            var domainWallet = new Domain.Wallets.Wallet(request.TenantId, request.UserId);

            await _repository.AddAsync(domainWallet, cancellationToken);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return domainWallet.Adapt<WalletCreatedResult>();
        }
    }
}
