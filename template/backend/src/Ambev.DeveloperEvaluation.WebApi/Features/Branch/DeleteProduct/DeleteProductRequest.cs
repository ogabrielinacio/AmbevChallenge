namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.DeleteProduct;

public record DeleteProductRequest
{
   public Guid BranchId { get; init; } 
   public Guid ProductId { get; init; }
}