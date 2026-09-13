namespace Mozzaro.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;

    Task<int> SaveChangesAsync();
}