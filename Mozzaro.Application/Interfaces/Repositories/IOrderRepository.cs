using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
}