using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.CartAggragate;

public class Cart : BaseEntity, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    
    private readonly List<CartItem> _items = new();
    
    public IReadOnlyCollection<CartItem> Items => _items;

    protected Cart(){}
    
    public Cart(Guid customerId)
    {
       CustomerId = customerId; 
    }
    
    public ValidationResultDetail Validate()
    {
        var validator = new CartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
        };
    }
    
    public void AddItem(Guid productId, int quantity)
    {
        if (quantity <= 0)
            throw new DomainRuleViolationException("Quantity must be greater than 0.");

        _items.Add(new CartItem(productId, quantity));
    }

    public void RemoveItem(Guid productId)
    {
        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem == null)
            throw new DomainNotFoundException("Item not found in cart.");

        _items.Remove(existingItem);
    }

    public void UpdateItemQuantity(Guid productId, int newQuantity)
    {
        if (newQuantity <= 0)
            throw new DomainRuleViolationException("Quantity must be greater than 0. To remove item use RemoveItem");

        var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem == null)
            throw new DomainRuleViolationException("Item not found in cart.");

        existingItem.SetQuantity(newQuantity);
    }
}