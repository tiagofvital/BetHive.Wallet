using FluentValidation;

namespace BetHive.Wallet.Application.Wallets.Commands.Deposit
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(x => x.TenantId).GreaterThan(0);
            RuleFor(x => x.WalletId).NotNull().NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
