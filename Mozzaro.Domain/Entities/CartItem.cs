namespace Mozzaro.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = null!;

    public int Quantity { get; set; }
}