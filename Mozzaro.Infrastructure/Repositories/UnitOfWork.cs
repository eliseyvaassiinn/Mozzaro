using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Infrastructure.Persistence;

namespace Mozzaro.Infrastructure.Repositories;

/// <summary>
/// coordinates repository operations and database changes.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly MozzaroDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(MozzaroDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// gets or creates a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="T">the entity type.</typeparam>
    /// <returns>the repository for the specified entity type.</returns>
    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);

        if (!_repositories.TryGetValue(type, out var repository))
        {
            repository = new Repository<T>(_context);
            _repositories[type] = repository;
        }

        return (IRepository<T>)repository;
    }

    /// <summary>
    /// saves all pending changes to the database.
    /// </summary>
    /// <returns>the number of affected records.</returns>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}