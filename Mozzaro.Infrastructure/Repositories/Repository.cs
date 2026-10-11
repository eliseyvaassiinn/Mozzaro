using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Infrastructure.Persistence;

namespace Mozzaro.Infrastructure.Repositories;

/// <summary>
/// provides generic data access operations using Entity Framework Core.
/// </summary>
/// <typeparam name="T">the entity type.</typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    private readonly MozzaroDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(MozzaroDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// gets all entities from the database.
    /// </summary>
    /// <returns>a collection of entities.</returns>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// gets an entity by its identifier.
    /// </summary>
    /// <param name="id">the entity identifier.</param>
    /// <returns>the entity if found; otherwise, null.</returns>
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// gets the first entity that matches the specified condition.
    /// </summary>
    /// <param name="predicate">the condition used to find the entity.</param>
    /// <returns>the matching entity if found; otherwise, null.</returns>
    public async Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// adds a new entity to the database context.
    /// </summary>
    /// <param name="entity">the entity to add.</param>
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    /// <summary>
    /// marks an entity as modified.
    /// </summary>
    /// <param name="entity">the entity to update.</param>
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    /// <summary>
    /// marks an entity for deletion.
    /// </summary>
    /// <param name="entity">the entity to delete.</param>
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}