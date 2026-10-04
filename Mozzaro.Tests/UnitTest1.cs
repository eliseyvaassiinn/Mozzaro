using Moq;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task RegisterAsync_CreatesUser_WhenEmailIsNew()
    {
        var repository = new Mock<IRepository<User>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        repository
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync((User?)null);

        repository
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(x => x.Repository<User>())
            .Returns(repository.Object);

        unitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var service = new UserService(unitOfWork.Object);

        var result = await service.RegisterAsync(
            "Mozzaro",
            "mozzaro@test.com",
            "password123");

        Assert.NotNull(result);
        Assert.Equal("Mozzaro", result.Name);
        Assert.Equal("mozzaro@test.com", result.Email);
        Assert.NotEqual("password123", result.PasswordHash);

        repository.Verify(
            x => x.AddAsync(It.IsAny<User>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_ReturnsNull_WhenEmailAlreadyExists()
    {
        var repository = new Mock<IRepository<User>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var existingUser = new User
        {
            Id = 1,
            Name = "Mozzaro",
            Email = "mozzaro@test.com",
            PasswordHash = "hash"
        };

        repository
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync(existingUser);

        unitOfWork
            .Setup(x => x.Repository<User>())
            .Returns(repository.Object);

        var service = new UserService(unitOfWork.Object);

        var result = await service.RegisterAsync(
            "Mozzaro",
            "mozzaro@test.com",
            "password123");

        Assert.Null(result);

        repository.Verify(
            x => x.AddAsync(It.IsAny<User>()),
            Times.Never);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }

    [Fact]
    public async Task RegisterAsync_NormalizesNameAndEmail()
    {
        var repository = new Mock<IRepository<User>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        repository
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync((User?)null);

        repository
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(x => x.Repository<User>())
            .Returns(repository.Object);

        unitOfWork
            .Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(1);

        var service = new UserService(unitOfWork.Object);

        var result = await service.RegisterAsync(
            "  Mozzaro  ",
            "  MOZZARO@TEST.COM  ",
            "password123");

        Assert.NotNull(result);
        Assert.Equal("Mozzaro", result.Name);
        Assert.Equal("mozzaro@test.com", result.Email);
    }

    [Fact]
    public async Task RegisterAsync_DoesNotSave_WhenEmailAlreadyExists()
    {
        var repository = new Mock<IRepository<User>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        repository
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User
            {
                Id = 1,
                Name = "Mozzaro",
                Email = "mozzaro@test.com",
                PasswordHash = "hash"
            });

        unitOfWork
            .Setup(x => x.Repository<User>())
            .Returns(repository.Object);

        var service = new UserService(unitOfWork.Object);

        var result = await service.RegisterAsync(
            "Another User",
            "mozzaro@test.com",
            "password123");

        Assert.Null(result);

        repository.Verify(
            x => x.AddAsync(It.IsAny<User>()),
            Times.Never);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(),
            Times.Never);
    }

    [Fact]
    public async Task LoginAsync_ReturnsUser_WhenCredentialsAreCorrect()
    {
        var repository = new Mock<IRepository<User>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var user = new User
        {
            Id = 1,
            Name = "Mozzaro",
            Email = "mozzaro@test.com",
            PasswordHash = "password-hash"
        };

        repository
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        unitOfWork
            .Setup(x => x.Repository<User>())
            .Returns(repository.Object);

        var service = new UserService(unitOfWork.Object);

        var result = await service.LoginAsync(
            "mozzaro@test.com",
            "password123");

        Assert.NotNull(result);
        Assert.Equal("Mozzaro", result.Name);
        Assert.Equal("mozzaro@test.com", result.Email);

        repository.Verify(
            x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()),
            Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenPasswordIsWrong()
    {
        var repository = new Mock<IRepository<User>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        repository
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync((User?)null);

        unitOfWork
            .Setup(x => x.Repository<User>())
            .Returns(repository.Object);

        var service = new UserService(unitOfWork.Object);

        var result = await service.LoginAsync(
            "mozzaro@test.com",
            "wrong-password");

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_NormalizesEmail()
    {
        var repository = new Mock<IRepository<User>>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var user = new User
        {
            Id = 1,
            Name = "Mozzaro",
            Email = "mozzaro@test.com",
            PasswordHash = "password-hash"
        };

        repository
            .Setup(x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()))
            .ReturnsAsync(user);

        unitOfWork
            .Setup(x => x.Repository<User>())
            .Returns(repository.Object);

        var service = new UserService(unitOfWork.Object);

        var result = await service.LoginAsync(
            "  MOZZARO@TEST.COM  ",
            "password123");

        Assert.NotNull(result);
        Assert.Equal("mozzaro@test.com", result.Email);

        repository.Verify(
            x => x.FirstOrDefaultAsync(
                It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>()),
            Times.Once);
    }
}