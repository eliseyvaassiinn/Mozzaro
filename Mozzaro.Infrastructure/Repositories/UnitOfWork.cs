using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Infrastructure.Persistence;

namespace Mozzaro.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MozzaroDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(MozzaroDbContext context)
    {
        _context = context;
    }

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

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}