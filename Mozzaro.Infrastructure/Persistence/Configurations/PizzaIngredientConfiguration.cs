using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Persistence.Configurations;

public class PizzaIngredientConfiguration : IEntityTypeConfiguration<PizzaIngredient>
{
    public void Configure(EntityTypeBuilder<PizzaIngredient> builder)
    {
        builder.HasKey(x => new { x.PizzaId, x.IngredientId });

        builder.HasOne(x => x.Pizza)
            .WithMany(x => x.PizzaIngredients)
            .HasForeignKey(x => x.PizzaId);

        builder.HasOne(x => x.Ingredient)
            .WithMany(x => x.PizzaIngredients)
            .HasForeignKey(x => x.IngredientId);
    }
}