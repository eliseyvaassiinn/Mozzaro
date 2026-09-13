using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _unitOfWork.Repository<Category>().GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Repository<Category>().GetByIdAsync(id);
    }

    public async Task AddAsync(Category category)
    {
        await _unitOfWork.Repository<Category>().AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _unitOfWork.Repository<Category>().Update(category);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);

        if (category is null)
            return;

        _unitOfWork.Repository<Category>().Delete(category);
        await _unitOfWork.SaveChangesAsync();
    }
}