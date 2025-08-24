namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; }
    public decimal Price => Items.Sum(x => x.Price * x.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    //Required for mapping 
    public ShoppingCart()
    {
    }
}