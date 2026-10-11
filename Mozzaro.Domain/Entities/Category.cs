namespace Mozzaro.Domain.Entities;

/// <summary>
/// represents a pizza category.
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}