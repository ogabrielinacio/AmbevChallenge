using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBranchRepository
{
    Task<Branch> AddAsync(Branch branch, CancellationToken cancellationToken = default);
    
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Branch?> GetByIdWithProductsAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<Branch?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    Task<bool> Update(Branch branch, CancellationToken cancellationToken = default);
    Task<bool> Remove(Guid branchId, CancellationToken cancellationToken = default); 
    
    Task<Product?> AddProductAsync(Guid branchId, Product product, CancellationToken cancellationToken = default);
    
    Task<bool> RemoveProductAsync(Guid branchId, Guid productId, CancellationToken cancellationToken = default);
    
    Task<bool> UpdateProductAsync(Guid branchId, Product updatedProduct, CancellationToken cancellationToken = default);
    
    Task<Product?> GetProductByIdAsync(Guid branchId, Guid productId, CancellationToken cancellationToken = default);
    
    Task<Product?> GetProductBranchByNameAsync(Guid branchId, string name, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Product>> GetProductsByBranchAsync(Guid branchId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Product>> GetProductsByCategoryAndBranchAsync(Guid branchId, string category,
        CancellationToken cancellationToken = default);

}