namespace Mozzaro.Domain.Entities;

/// <summary>
/// represents an item included in a customer order.
/// </summary>
public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}