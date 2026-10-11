using Microsoft.EntityFrameworkCore;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Persistence;

/// <summary>
/// represents the Entity Framework Core database context for Mozzaro.
/// </summary>
public class MozzaroDbContext : DbContext
{
    public MozzaroDbContext(DbContextOptions<MozzaroDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<PizzaIngredient> PizzaIngredients => Set<PizzaIngredient>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <summary>
    /// configures the database model and applies entity configurations.
    /// </summary>
    /// <param name="modelBuilder">the model builder used to configure the database model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MozzaroDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}