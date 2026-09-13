namespace Mozzaro.Domain.Entities;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<PizzaIngredient> PizzaIngredients { get; set; } =
        new List<PizzaIngredient>();
}