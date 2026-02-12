using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Services;

public interface ICartService
{
    // Get all items in cart
    Task<List<CartItem>> GetCartItemsAsync();

    // Add product to cart
    Task AddToCartAsync(int productId, int quantity = 1);

    // Remove product from cart
    Task RemoveFromCartAsync(int productId);

    // Update quantity of an item
    Task UpdateQuantityAsync(int productId, int quantity);

    // Clear entire cart
    Task ClearCartAsync();

    // Get cart totals
    Task<decimal> GetSubtotalAsync();
    Task<int> GetCartCountAsync();

    // Check if cart contains product
    Task<bool> ContainsProductAsync(int productId);
}
