namespace Mozzaro.Domain.Entities;

public class PizzaIngredient
{
    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = null!;

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
}