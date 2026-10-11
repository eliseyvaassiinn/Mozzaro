namespace Mozzaro.Domain.Entities;

/// <summary>
/// represents the relationship between a pizza and an ingredient.
/// </summary>
public class PizzaIngredient
{
    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = null!;

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
}