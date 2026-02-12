using Microsoft.EntityFrameworkCore;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            // Check if any products exist
            if (context.Products.Any())
            {
                Console.WriteLine("✅ Products already exist in database");
                return;
            }

            // Add sample products
            context.Products.AddRange(
                new Product
                {
                    Name = "Laptop",
                    Description = "High performance laptop for work and play",
                    Price = 45000,
                    Discount = 10,
                    ImageUrl = "/images/products/laptop.png",
                    StockQuantity = 10
                },
                new Product
                {
                    Name = "Headphones",
                    Description = "Wireless noise-cancelling headphones",
                    Price = 3500,
                    Discount = 15,
                    ImageUrl = "/images/products/headphones.jpeg",
                    StockQuantity = 25
                },
                new Product
                {
                    Name = "Mouse",
                    Description = "Wireless ergonomic mouse",
                    Price = 1200,
                    Discount = 5,
                    ImageUrl = "/images/products/mouse.jpg",
                    StockQuantity = 50
                },
                new Product
                {
                    Name = "Keyboard",
                    Description = "Mechanical gaming keyboard",
                    Price = 2500,
                    Discount = 20,
                    ImageUrl = "/images/products/keyboard.jpeg",
                    StockQuantity = 30
                }
            );
            
            context.SaveChanges();
            Console.WriteLine("✅ 4 products seeded successfully!");
        }
    }
}
