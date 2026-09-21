using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Services;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(
        int userId,
        string deliveryAddress,
        IEnumerable<(int PizzaId, int Quantity, decimal Price)> items);
}