using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Services;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
}
