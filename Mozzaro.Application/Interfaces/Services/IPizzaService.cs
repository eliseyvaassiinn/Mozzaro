using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Services;

/// <summary>
/// defines operations for managing pizzas.
/// </summary>
public interface IPizzaService
{
    /// <summary>
    /// gets all pizzas.
    /// </summary>
    /// <returns>a collection of pizzas.</returns>
    Task<IEnumerable<Pizza>> GetAllAsync();

    /// <summary>
    /// gets a pizza by its identifier.
    /// </summary>
    /// <param name="id">the pizza identifier.</param>
    /// <returns>the pizza if found; otherwise, null.</returns>
    Task<Pizza?> GetByIdAsync(int id);

    /// <summary>
    /// adds a new pizza.
    /// </summary>
    /// <param name="pizza">the pizza to add.</param>
    Task AddAsync(Pizza pizza);

    /// <summary>
    /// updates an existing pizza.
    /// </summary>
    /// <param name="pizza">the pizza to update.</param>
    Task UpdateAsync(Pizza pizza);

    /// <summary>
    /// deletes a pizza by its identifier.
    /// </summary>
    /// <param name="id">the pizza identifier.</param>
    Task DeleteAsync(int id);
}