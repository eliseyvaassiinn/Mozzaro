using Microsoft.EntityFrameworkCore;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Domain.Entities;
using Mozzaro.Infrastructure.Persistence;

namespace Mozzaro.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly MozzaroDbContext _context;

    public OrderRepository(MozzaroDbContext context)
        : base(context)
    {
        _context = context;
    }

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