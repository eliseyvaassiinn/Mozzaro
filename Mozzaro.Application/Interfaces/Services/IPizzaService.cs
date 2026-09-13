using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Services;

public interface IPizzaService
{
    Task<IEnumerable<Pizza>> GetAllAsync();

    Task<Pizza?> GetByIdAsync(int id);

    Task AddAsync(Pizza pizza);

    Task UpdateAsync(Pizza pizza);

    Task DeleteAsync(int id);
}