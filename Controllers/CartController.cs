using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApp.Models;
using OnlineShoppingApp.Services;

namespace OnlineShoppingApp.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IProductRepository _productRepository;

    public CartController(ICartService cartService, IProductRepository productRepository)
    {
        _cartService = cartService;
        _productRepository = productRepository;
    }

    // GET: /Cart/Index
    public async Task<IActionResult> Index()
    {
        var items = await _cartService.GetCartItemsAsync();
        var subtotal = await _cartService.GetSubtotalAsync();
        
        // Discount logic: 10% off if subtotal > 5000
        var discount = subtotal > 5000 ? subtotal * 0.10m : 0;
        var total = subtotal - discount;

        ViewBag.Subtotal = subtotal;
        ViewBag.Discount = discount;
        ViewBag.Total = total;
        
        return View(items);
    }

    // POST: /Cart/AddToCart
    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        await _cartService.AddToCartAsync(productId, quantity);
        TempData["Message"] = "Item added to cart!";
        return RedirectToAction("Index", "Products");
    }

    // POST: /Cart/RemoveFromCart
    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        await _cartService.RemoveFromCartAsync(productId);
        TempData["Message"] = "Item removed from cart!";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Cart/UpdateQuantity
    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
    {
        await _cartService.UpdateQuantityAsync(productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    // POST: /Cart/ClearCart
    [HttpPost]
    public async Task<IActionResult> ClearCart()
    {
        await _cartService.ClearCartAsync();
        TempData["Message"] = "Cart cleared!";
        return RedirectToAction(nameof(Index));
    }
}
