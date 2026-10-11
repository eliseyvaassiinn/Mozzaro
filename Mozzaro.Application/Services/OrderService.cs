using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Services;

/// <summary>
/// provides operations for creating and retrieving customer orders.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;

    public OrderService(
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// creates a new customer order.
    /// </summary>
    /// <param name="userId">the user identifier.</param>
    /// <param name="deliveryAddress">the delivery address.</param>
    /// <param name="items">the pizzas and quantities included in the order.</param>
    /// <returns>the created order.</returns>
    public async Task<Order> CreateOrderAsync(
        int userId,
        string deliveryAddress,
        IEnumerable<(int PizzaId, int Quantity, decimal Price)> items)
    {
        var orderItems = items
            .Select(item => new OrderItem
            {
                PizzaId = item.PizzaId,
                Quantity = item.Quantity,
                Price = item.Price
            })
            .ToList();

        var totalPrice = orderItems.Sum(
            item => item.Price * item.Quantity);

        var order = new Order
        {
            UserId = userId,
            Status = "Pending",
            TotalPrice = totalPrice,
            CreatedAt = DateTime.UtcNow,
            DeliveryAddress = deliveryAddress,
            OrderItems = orderItems
        };

        await _unitOfWork.Repository<Order>()
            .AddAsync(order);

        await _unitOfWork.SaveChangesAsync();

        return order;
    }

    /// <summary>
    /// gets all orders belonging to a user.
    /// </summary>
    /// <param name="userId">the user identifier.</param>
    /// <returns>the user's orders.</returns>
    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
    {
        return await _orderRepository.GetByUserIdAsync(userId);
    }
}