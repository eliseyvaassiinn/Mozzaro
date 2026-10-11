using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Services;

/// <summary>
/// defines operations for managing customer orders.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// creates a new customer order.
    /// </summary>
    /// <param name="userId">the user identifier.</param>
    /// <param name="deliveryAddress">the delivery address.</param>
    /// <param name="items">the pizzas and quantities included in the order.</param>
    /// <returns>the created order.</returns>
    Task<Order> CreateOrderAsync(
        int userId,
        string deliveryAddress,
        IEnumerable<(int PizzaId, int Quantity, decimal Price)> items);

    /// <summary>
    /// gets all orders belonging to a user.
    /// </summary>
    /// <param name="userId">the user identifier.</param>
    /// <returns>the user's orders.</returns>
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);
}