using Moq;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Tests;

public class OrderServiceTests
{
    [Fact]
    public async Task CreateOrderAsync_CreatesOrderWithCorrectTotal()
    {
        var repository = new Mock<IRepository<Order>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        Order? savedOrder = null;

        repository
            .Setup(x => x.AddAsync(It.IsAny<Order>()))
            .Callback<Order>(order => savedOrder = order)
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(x => x.Repository<Order>())
            .Returns(repository.Object);

        unitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var orderService = new OrderService(
            unitOfWork.Object,
            Mock.Of<IOrderRepository>());

        var items = new List<(int PizzaId, int Quantity, decimal Price)>
        {
            (1, 2, 10.99m),
            (2, 1, 7.50m)
        };

        var result = await orderService.CreateOrderAsync(
            5,
            "Calle Test 10",
            items);

        Assert.NotNull(result);
        Assert.NotNull(savedOrder);

        Assert.Equal(5, result.UserId);
        Assert.Equal("Calle Test 10", result.DeliveryAddress);
        Assert.Equal("Pending", result.Status);
        Assert.Equal(29.48m, result.TotalPrice);

        Assert.Equal(2, result.OrderItems.Count);

        repository.Verify(
            x => x.AddAsync(It.IsAny<Order>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task CreateOrderAsync_CreatesOrderItemsWithCorrectData()
    {
        var repository = new Mock<IRepository<Order>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork
            .Setup(x => x.Repository<Order>())
            .Returns(repository.Object);

        unitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var orderService = new OrderService(
            unitOfWork.Object,
            Mock.Of<IOrderRepository>());

        var items = new List<(int PizzaId, int Quantity, decimal Price)>
        {
            (3, 2, 12.50m),
            (4, 3, 8.00m)
        };

        var result = await orderService.CreateOrderAsync(
            1,
            "Test address",
            items);

        var firstItem = result.OrderItems.First();
        var secondItem = result.OrderItems.Last();

        Assert.Equal(3, firstItem.PizzaId);
        Assert.Equal(2, firstItem.Quantity);
        Assert.Equal(12.50m, firstItem.Price);

        Assert.Equal(4, secondItem.PizzaId);
        Assert.Equal(3, secondItem.Quantity);
        Assert.Equal(8.00m, secondItem.Price);

        Assert.Equal(49.00m, result.TotalPrice);
    }

    [Fact]
    public async Task CreateOrderAsync_CreatesOrderWithEmptyItems()
    {
        var repository = new Mock<IRepository<Order>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork
            .Setup(x => x.Repository<Order>())
            .Returns(repository.Object);

        unitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var orderService = new OrderService(
            unitOfWork.Object,
            Mock.Of<IOrderRepository>());

        var result = await orderService.CreateOrderAsync(
            5,
            "Test address",
            new List<(int PizzaId, int Quantity, decimal Price)>());

        Assert.NotNull(result);
        Assert.Empty(result.OrderItems);
        Assert.Equal(0m, result.TotalPrice);
        Assert.Equal(5, result.UserId);
        Assert.Equal("Pending", result.Status);
    }

    [Fact]
    public async Task CreateOrderAsync_CalculatesTotalForMultipleQuantities()
    {
        var repository = new Mock<IRepository<Order>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork
            .Setup(x => x.Repository<Order>())
            .Returns(repository.Object);

        unitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var orderService = new OrderService(
            unitOfWork.Object,
            Mock.Of<IOrderRepository>());

        var items = new List<(int PizzaId, int Quantity, decimal Price)>
        {
            (1, 5, 10.00m),
            (2, 4, 7.50m),
            (3, 2, 15.25m)
        };

        var result = await orderService.CreateOrderAsync(
            3,
            "Calle Test 20",
            items);

        Assert.Equal(110.50m, result.TotalPrice);
        Assert.Equal(3, result.OrderItems.Count);
    }

    [Fact]
    public async Task CreateOrderAsync_SavesOrderOnce()
    {
        var repository = new Mock<IRepository<Order>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork
            .Setup(x => x.Repository<Order>())
            .Returns(repository.Object);

        unitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var orderService = new OrderService(
            unitOfWork.Object,
            Mock.Of<IOrderRepository>());

        var items = new List<(int PizzaId, int Quantity, decimal Price)>
        {
            (1, 1, 10.00m)
        };

        await orderService.CreateOrderAsync(
            5,
            "Test address",
            items);

        repository.Verify(
            x => x.AddAsync(It.IsAny<Order>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task GetUserOrdersAsync_ReturnsEmptyList_WhenUserHasNoOrders()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var orderRepository = new Mock<IOrderRepository>();

        orderRepository
            .Setup(x => x.GetByUserIdAsync(10))
            .ReturnsAsync(new List<Order>());

        var orderService = new OrderService(
            unitOfWork.Object,
            orderRepository.Object);

        var result = await orderService.GetUserOrdersAsync(10);

        Assert.Empty(result);

        orderRepository.Verify(
            x => x.GetByUserIdAsync(10),
            Times.Once);
    }

    [Fact]
    public async Task GetUserOrdersAsync_RequestsOrdersForCorrectUser()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var orderRepository = new Mock<IOrderRepository>();

        orderRepository
            .Setup(x => x.GetByUserIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<Order>());

        var orderService = new OrderService(
            unitOfWork.Object,
            orderRepository.Object);

        await orderService.GetUserOrdersAsync(25);

        orderRepository.Verify(
            x => x.GetByUserIdAsync(25),
            Times.Once);
    }

    [Fact]
    public async Task GetUserOrdersAsync_ReturnsOrdersForUser()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        var orderRepository = new Mock<IOrderRepository>();

        var orders = new List<Order>
        {
            new()
            {
                Id = 1,
                UserId = 5,
                Status = "Pending",
                TotalPrice = 20.00m
            },
            new()
            {
                Id = 2,
                UserId = 5,
                Status = "Delivered",
                TotalPrice = 30.00m
            }
        };

        orderRepository
            .Setup(x => x.GetByUserIdAsync(5))
            .ReturnsAsync(orders);

        var orderService = new OrderService(
            unitOfWork.Object,
            orderRepository.Object);

        var result = await orderService.GetUserOrdersAsync(5);

        var resultList = result.ToList();

        Assert.Equal(2, resultList.Count);
        Assert.Equal(1, resultList[0].Id);
        Assert.Equal(2, resultList[1].Id);

        orderRepository.Verify(
            x => x.GetByUserIdAsync(5),
            Times.Once);
    }
}