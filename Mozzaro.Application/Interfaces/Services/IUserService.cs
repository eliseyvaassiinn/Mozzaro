using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Services;

public interface IUserService
{
    Task<User?> RegisterAsync(
        string name,
        string email,
        string password);

    Task<User?> LoginAsync(
        string email,
        string password);

    Task<User?> GetByIdAsync(int id);
}