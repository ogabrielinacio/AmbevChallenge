namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class SaleItem
{
   public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalWithoutDiscount => UnitPrice * Quantity;
    public decimal DiscountAmount => UnitPrice * Quantity * Discount;
    
    public decimal TotalWithDiscount => TotalWithoutDiscount - DiscountAmount;

    protected SaleItem() {} 
    public SaleItem(Guid productId, int quantity, decimal unitPrice, decimal discount)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

}