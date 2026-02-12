using Microsoft.AspNetCore.Mvc;
using OnlineShoppingApp.Services;

namespace OnlineShoppingApp.ViewComponents;

public class CartCountViewComponent : ViewComponent
{
    private readonly ICartService _cartService;

    public CartCountViewComponent(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var count = await _cartService.GetCartCountAsync();
        return View(count);
    }
}
