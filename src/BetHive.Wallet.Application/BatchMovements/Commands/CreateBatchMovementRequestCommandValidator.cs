using FluentValidation;

namespace BetHive.Wallet.Application.BatchMovements.Commands
{
    public class CreateBatchMovementRequestCommandValidator : AbstractValidator<CreateBatchMovementRequestCommand>
    {
        public CreateBatchMovementRequestCommandValidator()
        {
            RuleFor(x => x.TenantId).GreaterThan(0);
            RuleFor(x => x.ExternalId).NotNull().NotEmpty();
            RuleForEach(x => x.Movements).ChildRules(r =>
            {
                r.RuleFor(m => m.Amount).GreaterThan(0);
                r.RuleFor(m => m.UserId).NotNull().NotEmpty();
            });
        }
    }
}
