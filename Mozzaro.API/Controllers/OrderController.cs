using Microsoft.AspNetCore.Mvc;
using Mozzaro.Application.Interfaces.Services;

namespace Mozzaro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request)
    {
        if (request.UserId <= 0)
        {
            return BadRequest("Некорректный пользователь.");
        }

        if (string.IsNullOrWhiteSpace(request.DeliveryAddress))
        {
            return BadRequest("Адрес доставки обязателен.");
        }

        if (request.Items == null || request.Items.Count == 0)
        {
            return BadRequest("Корзина пуста.");
        }

        var order = await _orderService.CreateOrderAsync(
            request.UserId,
            request.DeliveryAddress,
            request.Items.Select(item =>
                (item.PizzaId, item.Quantity, item.Price)));

        return Ok(new
        {
            id = order.Id
        });
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserOrders(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Некорректный пользователь.");
        }

        var orders = await _orderService.GetUserOrdersAsync(userId);

        var result = orders.Select(order => new
        {
            id = order.Id,
            status = order.Status,
            totalPrice = order.TotalPrice,
            createdAt = order.CreatedAt,
            deliveryAddress = order.DeliveryAddress,

            items = order.OrderItems.Select(item => new
            {
                pizzaId = item.PizzaId,
                pizzaName = item.Pizza.Name,
                imageUrl = item.Pizza.ImageUrl,
                quantity = item.Quantity,
                price = item.Price
            })
        });

        return Ok(result);
    }
}

public class CreateOrderRequest
{
    public int UserId { get; set; }

    public string DeliveryAddress { get; set; } = string.Empty;

    public List<CreateOrderItemRequest> Items { get; set; } = new();
}

public class CreateOrderItemRequest
{
    public int PizzaId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}