using Microsoft.AspNetCore.Mvc;
using Mozzaro.Application.Interfaces.Services;

namespace Mozzaro.API.Controllers;

/// <summary>
/// provides API endpoints for creating and retrieving customer orders.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// creates a new customer order.
    /// </summary>
    /// <param name="request">the order creation request.</param>
    /// <returns>the identifier of the created order.</returns>
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

    /// <summary>
    /// gets all orders belonging to a specific user.
    /// </summary>
    /// <param name="userId">the user identifier.</param>
    /// <returns>the user's orders with their items.</returns>
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

/// <summary>
/// represents the data required to create a customer order.
/// </summary>
public class CreateOrderRequest
{
    /// <summary>
    /// gets or sets the identifier of the user placing the order.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// gets or sets the delivery address.
    /// </summary>
    public string DeliveryAddress { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the items included in the order.
    /// </summary>
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}

/// <summary>
/// represents a pizza item in an order creation request.
/// </summary>
public class CreateOrderItemRequest
{
    /// <summary>
    /// gets or sets the pizza identifier.
    /// </summary>
    public int PizzaId { get; set; }

    /// <summary>
    /// gets or sets the quantity of pizzas.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// gets or sets the price of one pizza.
    /// </summary>
    public decimal Price { get; set; }
}