namespace Mozzaro.Domain.Entities;

/// <summary>
/// represents a pizza item in a user's shopping cart.
/// </summary>
public class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = null!;

    public int Quantity { get; set; }
}