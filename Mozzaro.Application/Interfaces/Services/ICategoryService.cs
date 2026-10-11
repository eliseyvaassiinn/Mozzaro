using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Services;

/// <summary>
/// defines operations for managing pizza categories.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// gets all categories.
    /// </summary>
    /// <returns>a collection of categories.</returns>
    Task<IEnumerable<Category>> GetAllAsync();

    /// <summary>
    /// gets a category by its identifier.
    /// </summary>
    /// <param name="id">the category identifier.</param>
    /// <returns>the category if found; otherwise, null.</returns>
    Task<Category?> GetByIdAsync(int id);

    /// <summary>
    /// adds a new category.
    /// </summary>
    /// <param name="category">the category to add.</param>
    Task AddAsync(Category category);

    /// <summary>
    /// updates an existing category.
    /// </summary>
    /// <param name="category">the category to update.</param>
    Task UpdateAsync(Category category);

    /// <summary>
    /// deletes a category by its identifier.
    /// </summary>
    /// <param name="id">the category identifier.</param>
    Task DeleteAsync(int id);
}