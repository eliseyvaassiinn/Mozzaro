using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Repositories;

/// <summary>
/// defines data access operations for orders.
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// gets all orders belonging to a specific user.
    /// </summary>
    /// <param name="userId">the user identifier.</param>
    /// <returns>the user's orders.</returns>
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
}