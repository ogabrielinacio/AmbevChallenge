using Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Context;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;
    
    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
    }

    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Branches
            .Include(b => b.Products)
            .Include(b => b.Users)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<bool> Update(Branch branch, CancellationToken cancellationToken = default)
    {
        var exists = await GetByIdAsync(branch.Id, cancellationToken); 

        if (exists == null)
            return false;
        _context.Branches.Update(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> Remove(Branch branch, CancellationToken cancellationToken = default)
    {
        var exists = await GetByIdAsync(branch.Id, cancellationToken); 

        if (exists == null)
            return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
    
    public async Task<Product?> AddProductAsync(Guid branchId, Product product, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(branchId, cancellationToken);
        if (branch is null)
            return null;

        var addedProduct = branch.AddProduct(
            product.Title,
            product.Price,
            product.Description,
            product.Category,
            product.Image,
            product.Rating
        );

        await Update(branch, cancellationToken);

        return addedProduct;
    }

    public async Task<bool> RemoveProductAsync(Guid branchId, Guid productId, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(branchId, cancellationToken);
        if (branch is null)
            return false;
        branch.RemoveProduct(productId);
      
        return await Update(branch, cancellationToken);
    }

    public async Task<bool> UpdateProductAsync(Guid branchId, Product updatedProduct, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(branchId, cancellationToken);
        if (branch is null)
            return false;
        
        branch.UpdateProduct(updatedProduct);
      
        return await Update(branch, cancellationToken);
    }
    
    public async Task<IEnumerable<Product>> GetProductsByBranchAsync(Guid branchId, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.BranchId == branchId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Product>> GetProductsByCategoryAndBranchAsync(Guid branchId, string category, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.BranchId == branchId && p.Category == category)
            .ToListAsync(cancellationToken); 
    }
}