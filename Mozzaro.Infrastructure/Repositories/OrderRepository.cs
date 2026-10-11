using Microsoft.EntityFrameworkCore;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Domain.Entities;
using Mozzaro.Infrastructure.Persistence;

namespace Mozzaro.Infrastructure.Repositories;

/// <summary>
/// provides data access operations for customer orders.
/// </summary>
public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly MozzaroDbContext _context;

    public OrderRepository(MozzaroDbContext context)
        : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// gets all orders belonging to a specific user.
    /// </summary>
    /// <param name="userId">the user identifier.</param>
    /// <returns>the user's orders with their order items and pizzas.</returns>
    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Where(order => order.UserId == userId)
            .Include(order => order.OrderItems)
            .ThenInclude(item => item.Pizza)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync();
    }
}