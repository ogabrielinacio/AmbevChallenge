namespace Ambev.DeveloperEvaluation.WebApi.Features.Branch.CreateProduct;

public record CreateProductRequest
{
   public Guid BranchId { get; init; }
   
   public string Title { get; init; } = string.Empty;
   
   public string Description { get; init; } = string.Empty;
   
   public decimal Price { get; init; }
   
   public string Category { get; init; } = string.Empty;
   
   public string Image { get; init; } = string.Empty;
}