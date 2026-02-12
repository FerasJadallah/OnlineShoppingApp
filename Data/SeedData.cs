using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        // Return early if products already exist
        if (context.Products.Any())
        {
            return;
        }

        context.Products.AddRange(
            new Product
            {
                Name = "Laptop",
                Description = "High performance laptop for work and play",
                Price = 45000,
                Discount = 10,
                ImageUrl = "https://placehold.co/300x200/2563eb/white?text=Laptop",
                StockQuantity = 10
            },
            new Product
            {
                Name = "Headphones",
                Description = "Wireless noise-cancelling headphones",
                Price = 3500,
                Discount = 15,
                ImageUrl = "https://placehold.co/300x200/7c3aed/white?text=Headphones",
                StockQuantity = 25
            },
            new Product
            {
                Name = "Mouse",
                Description = "Wireless ergonomic mouse",
                Price = 1200,
                Discount = 5,
                ImageUrl = "https://placehold.co/300x200/059669/white?text=Mouse",
                StockQuantity = 50
            },
            new Product
            {
                Name = "Keyboard",
                Description = "Mechanical gaming keyboard",
                Price = 2500,
                Discount = 20,
                ImageUrl = "https://placehold.co/300x200/dc2626/white?text=Keyboard",
                StockQuantity = 30
            }
        );

        context.SaveChanges();
        Console.WriteLine("✅ Products seeded successfully!");
    }
}
