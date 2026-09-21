using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}