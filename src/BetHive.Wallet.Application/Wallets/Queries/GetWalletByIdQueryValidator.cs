using BetHive.Wallet.Application.Wallets.Queries;

using FluentValidation;

namespace BetHive.Wallet.Application.Wallets.Commands.Withdraw
{
    public class GetWalletByIdQueryValidator : AbstractValidator<GetWalletByIdQuery>
    {
        public GetWalletByIdQueryValidator()
        {
            RuleFor(x => x.TenantId).GreaterThan(0);
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
