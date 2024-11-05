using FluentValidation;

namespace BetHive.Wallet.Application.Wallets.Commands.CreateWallet
{
    internal class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator()
        {
            RuleFor(x => x.TenantId).GreaterThan(0);
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
}
