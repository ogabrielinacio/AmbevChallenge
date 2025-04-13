using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates.SaleAggregate;

public class Sale : BaseEntity, IAggregateRoot
{
    public long SaleNumber { get; private set; }
    public Guid CustomerId { get; private set; }
    
    public Guid BranchId { get; private set; }
    public bool Cancelled { get; private set; }

    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items;

    public decimal TotalSaleAmount => _items.Sum(item => item.TotalWithDiscount );
    
    public Sale(long saleNumber,  Guid customerId, Guid branchId)
    {
        SaleNumber = saleNumber;
        CustomerId = customerId; 
        BranchId = branchId;
        Cancelled = false;
    }
    
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }


    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (quantity < 1)
            throw new DomainRuleViolationException("Quantity must be at least 1");

        if (quantity > 20)
            throw new DomainRuleViolationException("Cannot sell more than 20 items of the same product");

        decimal discount = 0;

         
        if (quantity >= 10)
            discount = 0.20m;
        else if (quantity >= 4)
            discount = 0.10m;

        _items.Add(new SaleItem(productId, quantity,  unitPrice, discount));
    }


    public void Cancel() => Cancelled = true;

    protected Sale() { } 
}