using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Services;

/// <summary>
/// provides operations for managing pizzas.
/// </summary>
public class PizzaService : IPizzaService
{
    private readonly IUnitOfWork _unitOfWork;

    public PizzaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// gets all pizzas.
    /// </summary>
    /// <returns>a collection of pizzas.</returns>
    public async Task<IEnumerable<Pizza>> GetAllAsync()
    {
        return await _unitOfWork.Repository<Pizza>().GetAllAsync();
    }

    /// <summary>
    /// gets a pizza by its identifier.
    /// </summary>
    /// <param name="id">the pizza identifier.</param>
    /// <returns>the pizza if found; otherwise, null.</returns>
    public async Task<Pizza?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Repository<Pizza>().GetByIdAsync(id);
    }

    /// <summary>
    /// adds a new pizza.
    /// </summary>
    /// <param name="pizza">the pizza to add.</param>
    public async Task AddAsync(Pizza pizza)
    {
        await _unitOfWork.Repository<Pizza>().AddAsync(pizza);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// updates an existing pizza.
    /// </summary>
    /// <param name="pizza">the pizza to update.</param>
    public async Task UpdateAsync(Pizza pizza)
    {
        _unitOfWork.Repository<Pizza>().Update(pizza);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// deletes a pizza by its identifier.
    /// </summary>
    /// <param name="id">the pizza identifier.</param>
    public async Task DeleteAsync(int id)
    {
        var pizza = await _unitOfWork.Repository<Pizza>().GetByIdAsync(id);

        if (pizza is null)
            return;

        _unitOfWork.Repository<Pizza>().Delete(pizza);
        await _unitOfWork.SaveChangesAsync();
    }
}