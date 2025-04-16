using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
   public CreateProductRequestValidator()
   {
       RuleFor(x => x.BranchId)
           .NotEqual(Guid.Empty)
           .WithMessage("BranchId is required.");
       
         RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(p => p.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(50).WithMessage("Category must not exceed 50 characters.");

        RuleFor(p => p.Image)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Image must be a valid URL."); 
   } 
}