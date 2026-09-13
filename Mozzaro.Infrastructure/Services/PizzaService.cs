using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Services;

public class PizzaService : IPizzaService
{
    private readonly IUnitOfWork _unitOfWork;

    public PizzaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Pizza>> GetAllAsync()
    {
        return await _unitOfWork.Repository<Pizza>().GetAllAsync();
    }

    public async Task<Pizza?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Repository<Pizza>().GetByIdAsync(id);
    }

    public async Task AddAsync(Pizza pizza)
    {
        await _unitOfWork.Repository<Pizza>().AddAsync(pizza);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Pizza pizza)
    {
        _unitOfWork.Repository<Pizza>().Update(pizza);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pizza = await _unitOfWork.Repository<Pizza>().GetByIdAsync(id);

        if (pizza is null)
            return;

        _unitOfWork.Repository<Pizza>().Delete(pizza);
        await _unitOfWork.SaveChangesAsync();
    }
}