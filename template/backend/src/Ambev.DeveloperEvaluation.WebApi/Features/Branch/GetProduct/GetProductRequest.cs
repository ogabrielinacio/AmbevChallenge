namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.GetProduct;

public record GetProductRequest
{
   public Guid BranchId { get; init; } 
   public Guid ProductId { get; init; } 
}