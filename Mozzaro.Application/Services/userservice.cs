using System.Security.Cryptography;
using System.Text;
using Mozzaro.Application.Interfaces.Repositories;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Services;

/// <summary>
/// provides operations for user registration and authentication.
/// </summary>
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// registers a new user.
    /// </summary>
    /// <param name="name">the user's name.</param>
    /// <param name="email">the user's email address.</param>
    /// <param name="password">the user's password.</param>
    /// <returns>the registered user if successful; otherwise, null.</returns>
    public async Task<User?> RegisterAsync(
        string name,
        string email,
        string password)
    {
        var normalizedEmail = email.Trim().ToLower();

        var existingUser = await _unitOfWork
            .Repository<User>()
            .FirstOrDefaultAsync(x => x.Email == normalizedEmail);

        if (existingUser != null)
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

    /// <summary>
    /// authenticates a user with email and password.
    /// </summary>
    /// <param name="email">the user's email address.</param>
    /// <param name="password">the user's password.</param>
    /// <returns>the authenticated user if credentials are valid; otherwise, null.</returns>
    public async Task<User?> LoginAsync(
        string email,
        string password)
    {
        var normalizedEmail = email.Trim().ToLower();
        var passwordHash = HashPassword(password);

        return await _unitOfWork
            .Repository<User>()
            .FirstOrDefaultAsync(x =>
                x.Email == normalizedEmail &&
                x.PasswordHash == passwordHash);
    }

    /// <summary>
    /// gets a user by their identifier.
    /// </summary>
    /// <param name="id">the user identifier.</param>
    /// <returns>the user if found; otherwise, null.</returns>
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _unitOfWork
            .Repository<User>()
            .GetByIdAsync(id);
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
}