using System.Security.Cryptography;
using System.Text;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> RegisterAsync(
        string name,
        string email,
        string password)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var normalizedEmail = email.Trim().ToLower();

        if (users.Any(x => x.Email == normalizedEmail))
        {
            return null;
        }

        var user = new User
        {
            Name = name.Trim(),
            Email = normalizedEmail,
            PasswordHash = HashPassword(password)
        };

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return user;
    }

    public async Task<User?> LoginAsync(
        string email,
        string password)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var normalizedEmail = email.Trim().ToLower();
        var passwordHash = HashPassword(password);

        return users.FirstOrDefault(x =>
            x.Email == normalizedEmail &&
            x.PasswordHash == passwordHash);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _unitOfWork.Repository<User>().GetByIdAsync(id);
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}