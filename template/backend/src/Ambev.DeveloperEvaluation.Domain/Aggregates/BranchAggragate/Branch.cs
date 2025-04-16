using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.BranchAggragate;

public class Branch : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; } 
    
    public Address Address { get; private set; }
    
    public Guid OwnerId { get; private set; } 
    
    private readonly List<Product> _products = new();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    
    private readonly List<BranchUser> _users = new();
    public IReadOnlyCollection<BranchUser> Users => _users.AsReadOnly();

    protected Branch() { }
    
    public Branch(string name, Address address, Guid ownerId)
    {
        Name = name;
        Address = address;
        OwnerId = ownerId;
    }
    
    public ValidationResultDetail Validate()
    {
        var validator = new BranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }

    public Product AddProduct(string title, decimal price, string description, string category, string image)
    {
        var product = new Product(Id, title, price, description, category, image);
    
        var validation = product.Validate();
        if (!validation.IsValid)
            throw new DomainRuleViolationException("Product is not valid");

        _products.Add(product);
        return product;
    }

    public void RemoveProduct(Guid productId)
    {
        var product = _products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
            _products.Remove(product);
    }

    public void UpdateProduct(Product updatedProduct)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);
        if (existingProduct == null)
            throw new DomainNotFoundException("Product not found");

        existingProduct.UpdateTitle(updatedProduct.Title);
        existingProduct.UpdatePrice(updatedProduct.Price);
        existingProduct.UpdateDescription(updatedProduct.Description);
        existingProduct.UpdateCategory(updatedProduct.Category);
        existingProduct.UpdateImage(updatedProduct.Image);
        existingProduct.UpdateRating(updatedProduct.Rating);
    }
    
    public void AddUser(Guid userId, BranchUserRole role)
    {
        if (_users.Any(u => u.UserId == userId))
            throw new DomainRuleViolationException("User already exists in this branch.");

        _users.Add(new BranchUser(userId, role));
    }

    public void RemoveUser(Guid userId)
    {
        var branchUser = _users.FirstOrDefault(u => u.UserId == userId);
        if (branchUser == null)
            throw new DomainNotFoundException("User not found in this branch.");

        _users.Remove(branchUser);
    }

    public void ChangeOwner(Guid newOwnerId)
    {
        OwnerId = newOwnerId;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainRuleViolationException("Branch name cannot be empty");
        Name = name;
    }

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new DomainRuleViolationException("Address cannot be empty");
    }
    
    public bool HasPermission(Guid userId)
    {
        if (IsOwner(userId))
            return true;

        if(IsManager(userId))
            return true;
        return false;
    }

    public bool IsOwner(Guid userId)
    {
        return OwnerId == userId;
    }

    public bool IsManager(Guid userId)
    {
        return _users.Any(u => u.UserId == userId && u.Role == UserRole.Manager);
    }

    
    
}