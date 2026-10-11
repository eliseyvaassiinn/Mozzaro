using System.Linq.Expressions;

namespace Mozzaro.Application.Interfaces.Repositories;

/// <summary>
/// defines common data access operations for entities.
/// </summary>
/// <typeparam name="T">the entity type.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// gets all entities.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// gets an entity by its identifier.
    /// </summary>
    /// <param name="id">the entity identifier.</param>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// gets the first entity that matches the specified condition.
    /// </summary>
    /// <param name="predicate">the condition used to find the entity.</param>
    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate);

    /// <summary>
    /// adds a new entity.
    /// </summary>
    /// <param name="entity">the entity to add.</param>
    Task AddAsync(T entity);

    /// <summary>
    /// updates an existing entity.
    /// </summary>
    /// <param name="entity">the entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// deletes an entity.
    /// </summary>
    /// <param name="entity">the entity to delete.</param>
    void Delete(T entity);
}