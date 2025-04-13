using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public Guid BranchId { get; private set; }
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public string Image { get; private set; }
    public Rating Rating { get; private set; }

    protected Product() { }
    
    public Product(Guid branchId,string title, decimal price, string description, string category, string image, Rating rating)
    {
        BranchId = branchId;
        UpdateTitle(title);
        UpdatePrice(price);
        UpdateDescription(description);
        UpdateCategory(category);
        UpdateImage(image);
        UpdateRating(rating);
    }
    
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
    
   
    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainRuleViolationException("Title cannot be empty");

        Title = title;
    }

    public void UpdatePrice(decimal price)
    {
        if (price <= 0)
            throw new DomainRuleViolationException("Price must be greater than zero");

        Price = price;
    }

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainRuleViolationException("Description cannot be empty");

        Description = description;
    }

    public void UpdateCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            throw new DomainRuleViolationException("Category cannot be empty");

        Category = category;
    }

    public void UpdateImage(string image)
    {
        if (string.IsNullOrWhiteSpace(image))
            throw new DomainRuleViolationException("Image cannot be empty");

        Image = image;
    }

    public void UpdateRating(Rating rating)
    {
        Rating = rating ?? throw new DomainRuleViolationException("Rating cannot be null");
    }
    
    
}