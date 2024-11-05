using BetHive.Wallet.Application.Wallets.Queries;

using FluentValidation;

namespace BetHive.Wallet.Application.Wallets.Commands.Withdraw
{
    public class GetWalletsQueryValidator : AbstractValidator<GetWalletsQuery>
    {
        public GetWalletsQueryValidator()
        {
            RuleFor(x => x.TenantId).GreaterThan(0);
        }
    }
}
