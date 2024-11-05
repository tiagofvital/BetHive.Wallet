using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Application.Common.Security.Permissions;
using BetHive.Wallet.Application.Common.Security.Policies;
using BetHive.Wallet.Application.Common.Security.Request;
using BetHive.Wallet.Application.Wallets.Common;
using BetHive.Wallet.Domain.Wallets;

using ErrorOr;

using Mapster;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Queries;

[Authorize(Permissions = Permission.Wallet.Read, Policies = Policy.SelfOrAdmin)]
public class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, ErrorOr<WalletResult>>
{
    private readonly IWalletsRepository _walletsRepository;

    public GetWalletByIdQueryHandler(IWalletsRepository walletsRepository)
    {
        _walletsRepository = walletsRepository;
    }

    public async Task<ErrorOr<WalletResult>> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await this._walletsRepository.GetByIdAsync(
            request.Id,
            request.TenantId,
            cancellationToken);

        if (wallet is null)
        {
            return WalletErrors.NotFound;
        }

        return wallet.Adapt<WalletResult>();
    }
}
