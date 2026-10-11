namespace Mozzaro.Application.Interfaces.Repositories;

/// <summary>
/// defines a unit of work for managing repositories and saving changes.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// gets a repository for the specified entity type.
    /// </summary>
    /// <typeparam name="T">the entity type.</typeparam>
    /// <returns>the repository for the specified entity type.</returns>
    IRepository<T> Repository<T>() where T : class;

    /// <summary>
    /// saves all pending changes.
    /// </summary>
    /// <returns>the number of affected records.</returns>
    Task<int> SaveChangesAsync();
}