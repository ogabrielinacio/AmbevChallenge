using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.UseCases.Commands.Branches.UpdateProduct;

public record UpdateProductCommand : IRequest<UpdateProductResponse>
{
    public Guid Id { get; init; }
    
    public string Title { get; init; } = string.Empty;
    
    public string Description { get; init; } = string.Empty;
    
    public decimal Price { get; init; }
    
    public string Category { get; init; } = string.Empty;
    
    public string Image { get; init; } = string.Empty;
    
    public Guid BranchId { get; init; }
    public Guid RequestedBy { get; init; }
    
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    } 
}
