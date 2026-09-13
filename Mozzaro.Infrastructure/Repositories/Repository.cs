using Microsoft.EntityFrameworkCore;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Infrastructure.Persistence;

namespace Mozzaro.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MozzaroDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(MozzaroDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}