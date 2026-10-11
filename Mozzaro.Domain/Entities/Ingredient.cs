namespace Mozzaro.Domain.Entities;

/// <summary>
/// represents an ingredient used in pizzas.
/// </summary>
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<PizzaIngredient> PizzaIngredients { get; set; } =
        new List<PizzaIngredient>();
}