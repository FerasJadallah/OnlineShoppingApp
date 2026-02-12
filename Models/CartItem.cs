namespace OnlineShoppingApp.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal DiscountPercentage { get; set; }
    
    // Calculated properties
    public decimal LineTotal => UnitPrice * Quantity;
    public decimal DiscountAmount => LineTotal * (DiscountPercentage / 100);
    public decimal FinalPrice => LineTotal - DiscountAmount;
}
