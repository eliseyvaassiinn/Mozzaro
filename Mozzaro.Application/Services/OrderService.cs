using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Services;

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

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
    {
        return await _orderRepository.GetByUserIdAsync(userId);
    }
}