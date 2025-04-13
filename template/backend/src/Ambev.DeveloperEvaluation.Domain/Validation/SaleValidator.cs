using Ambev.DeveloperEvaluation.Domain.Aggregates.SaleAggregate;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .GreaterThan(0)
            .WithMessage("Sale number must be greater than zero.");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("Customer ID is required.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required.");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("At least one item is required in a sale.");

        RuleForEach(sale => sale.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 items of the same product.");

            item.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero.");
        });
    }
}