using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Persistence.Configurations;

public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Price)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(500);

        builder.Property(x => x.IsAvailable)
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Pizzas)
            .HasForeignKey(x => x.CategoryId);

        builder.HasMany(x => x.PizzaIngredients)
            .WithOne(x => x.Pizza)
            .HasForeignKey(x => x.PizzaId);

        builder.HasMany(x => x.CartItems)
            .WithOne(x => x.Pizza)
            .HasForeignKey(x => x.PizzaId);

        builder.HasMany(x => x.OrderItems)
            .WithOne(x => x.Pizza)
            .HasForeignKey(x => x.PizzaId);
    }
}