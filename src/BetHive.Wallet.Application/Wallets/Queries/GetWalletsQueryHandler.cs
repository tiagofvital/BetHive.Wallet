using BetHive.Wallet.Application.Common.Interfaces;
using BetHive.Wallet.Application.Common.Security.Permissions;
using BetHive.Wallet.Application.Common.Security.Policies;
using BetHive.Wallet.Application.Common.Security.Request;
using BetHive.Wallet.Application.Wallets.Common;

using ErrorOr;

using Mapster;

using MediatR;

namespace BetHive.Wallet.Application.Wallets.Queries;

[Authorize(Permissions = Permission.Wallet.Read, Policies = Policy.SelfOrAdmin)]
public class GetWalletsQueryHandler : IRequestHandler<GetWalletsQuery, ErrorOr<IEnumerable<WalletResult>>>
{
    private readonly IWalletsRepository _walletsRepository;

    public GetWalletsQueryHandler(IWalletsRepository walletsRepository)
    {
        _walletsRepository = walletsRepository;
    }

    public async Task<ErrorOr<IEnumerable<WalletResult>>> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
    {
        var wallets = await this._walletsRepository.GetAsync(request.TenantId, request.UserId, cancellationToken);

        return wallets
            .Select(w => w.Adapt<WalletResult>())
            .ToList();
    }
}
