using System.Text.Json;
using OnlineShoppingApp.Data;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Services;

public class SessionCartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _context;
    private const string CartSessionKey = "Cart";

    public SessionCartService(
        IHttpContextAccessor httpContextAccessor,
        AppDbContext context)
    {
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session 
        ?? throw new InvalidOperationException("Session not available");

    public Task<List<CartItem>> GetCartItemsAsync()
    {
        var cartJson = Session.GetString(CartSessionKey);
        if (string.IsNullOrEmpty(cartJson))
            return Task.FromResult(new List<CartItem>());

        var items = JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        return Task.FromResult(items);
    }

    private Task SaveCartItemsAsync(List<CartItem> items)
    {
        var cartJson = JsonSerializer.Serialize(items);
        Session.SetString(CartSessionKey, cartJson);
        return Task.CompletedTask;
    }

    public async Task AddToCartAsync(int productId, int quantity = 1)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null) 
            throw new ArgumentException($"Product with ID {productId} not found");

        var cart = await GetCartItemsAsync();
        var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity,
                DiscountPercentage = product.Discount
            });
        }

        await SaveCartItemsAsync(cart);
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cart = await GetCartItemsAsync();
        cart.RemoveAll(x => x.ProductId == productId);
        await SaveCartItemsAsync(cart);
    }

    public async Task UpdateQuantityAsync(int productId, int quantity)
    {
        if (quantity <= 0) 
            throw new ArgumentException("Quantity must be positive");

        var cart = await GetCartItemsAsync();
        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        
        if (item != null)
        {
            item.Quantity = quantity;
            await SaveCartItemsAsync(cart);
        }
    }

    public async Task ClearCartAsync()
    {
        await SaveCartItemsAsync(new List<CartItem>());
    }

    public async Task<decimal> GetSubtotalAsync()
    {
        var cart = await GetCartItemsAsync();
        return cart.Sum(x => x.FinalPrice);
    }

    public async Task<int> GetCartCountAsync()
    {
        var cart = await GetCartItemsAsync();
        return cart.Sum(x => x.Quantity);
    }

    public async Task<bool> ContainsProductAsync(int productId)
    {
        var cart = await GetCartItemsAsync();
        return cart.Any(x => x.ProductId == productId);
    }
}
