using Ambev.DeveloperEvaluation.Domain.Aggregates.CartAggragate;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CartValidator : AbstractValidator<Cart>
{
    public CartValidator()
    {
        RuleFor(cart => cart.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(cart => cart.Items)
            .NotEmpty().WithMessage("Cart must contain at least one item.");

        When(cart => cart.Items.Any(), () =>
        {
            RuleForEach(cart => cart.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Item quantity must be greater than zero.");

                item.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("Product ID is required.");
            });
        });
    }
}