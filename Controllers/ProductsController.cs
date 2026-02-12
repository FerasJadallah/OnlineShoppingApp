using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp.Data;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.ToListAsync();
        Console.WriteLine($"========== DEBUG ==========");
        Console.WriteLine($"✅ Found {products.Count} products in database");
        foreach (var p in products)
        {
            Console.WriteLine($"   - {p.Name}: ${p.Price}");
        }
        Console.WriteLine($"============================");
        return View(products);
    }
}
