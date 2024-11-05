using BetHive.Wallet.Application.Wallets.Commands.Deposit;

using FluentValidation;

namespace BetHive.Wallet.Application.Wallets.Commands.Withdraw
{
    public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator()
        {
            RuleFor(x => x.TenantId).GreaterThan(0);
            RuleFor(x => x.WalletId).NotNull().NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
