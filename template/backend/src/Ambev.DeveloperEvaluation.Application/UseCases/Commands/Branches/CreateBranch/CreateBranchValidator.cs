using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.CreateBranch;

public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(80).WithMessage("Branch name must be at most 80 characters long.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required.");

        RuleFor(x => x.Address.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(80).WithMessage("Street must be at most 80 characters long.");

        RuleFor(x => x.Address.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City must be at most 50 characters long.");

        RuleFor(x => x.Address.State)
            .NotEmpty().WithMessage("State is required.")
            .Length(2).WithMessage("State must be exactly 2 characters long.");

        RuleFor(x => x.Address.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(50).WithMessage("Country must be at most 50 characters long.");

        RuleFor(x => x.Address.ZipCode)
            .NotEmpty().WithMessage("Zip code is required.")
            .MaximumLength(10).WithMessage("Zip code must be at most 10 characters long.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
}