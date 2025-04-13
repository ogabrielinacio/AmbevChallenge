using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;


public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(80).WithMessage("Branch name must not exceed 80 characters.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Branch address is required.");

        RuleFor(x => x.OwnerId)
            .NotEqual(Guid.Empty).WithMessage("Owner ID is required.");
    }
}