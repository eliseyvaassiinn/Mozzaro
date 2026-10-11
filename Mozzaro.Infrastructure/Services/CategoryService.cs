using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Infrastructure.Services;

/// <summary>
/// provides operations for managing pizza categories.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// gets all categories.
    /// </summary>
    /// <returns>a collection of categories.</returns>
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _unitOfWork.Repository<Category>().GetAllAsync();
    }

    /// <summary>
    /// gets a category by its identifier.
    /// </summary>
    /// <param name="id">the category identifier.</param>
    /// <returns>the category if found; otherwise, null.</returns>
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Repository<Category>().GetByIdAsync(id);
    }

    /// <summary>
    /// adds a new category.
    /// </summary>
    /// <param name="category">the category to add.</param>
    public async Task AddAsync(Category category)
    {
        await _unitOfWork.Repository<Category>().AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// updates an existing category.
    /// </summary>
    /// <param name="category">the category to update.</param>
    public async Task UpdateAsync(Category category)
    {
        _unitOfWork.Repository<Category>().Update(category);
        await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    /// deletes a category by its identifier.
    /// </summary>
    /// <param name="id">the category identifier.</param>
    public async Task DeleteAsync(int id)
    {
        var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);

        if (category is null)
            return;

        _unitOfWork.Repository<Category>().Delete(category);
        await _unitOfWork.SaveChangesAsync();
    }
}