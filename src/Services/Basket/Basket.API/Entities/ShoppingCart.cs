namespace Basket.API.Entities;

public class ShoppingCart
{
    public string? Username { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = new();

    public decimal TotalPrice
    {
        get =>
            Items.Select(x => x.Quantity * x.Price).Sum();
    }

    public ShoppingCart()
    {
    }

    public ShoppingCart(string username)
    {
        Username = username;
    }
}