namespace OnlineShoppingApp.Services;

public static class DiscountPolicy
{
    public const decimal Threshold = 5000m;
    public const decimal Percent = 0.10m; // 10%

    public static decimal CalculateDiscount(decimal subtotal)
        => subtotal >= Threshold ? subtotal * Percent : 0m;
}
