namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class CartItem
{ 
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }


    public CartItem(Guid productId, int qty)
    {
        ProductId = productId;
        Quantity = qty;
    } 
    
    public void SetQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");

        Quantity = quantity;
    }
}