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

        return Ok(order);
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