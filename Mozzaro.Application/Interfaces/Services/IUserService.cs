using Mozzaro.Domain.Entities;

namespace Mozzaro.Application.Interfaces.Services;

/// <summary>
/// defines operations for managing users and authentication.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// registers a new user.
    /// </summary>
    /// <param name="name">the user's name.</param>
    /// <param name="email">the user's email address.</param>
    /// <param name="password">the user's password.</param>
    /// <returns>the registered user if successful; otherwise, null.</returns>
    Task<User?> RegisterAsync(
        string name,
        string email,
        string password);

    /// <summary>
    /// authenticates a user with email and password.
    /// </summary>
    /// <param name="email">the user's email address.</param>
    /// <param name="password">the user's password.</param>
    /// <returns>the authenticated user if credentials are valid; otherwise, null.</returns>
    Task<User?> LoginAsync(
        string email,
        string password);

    /// <summary>
    /// gets a user by their identifier.
    /// </summary>
    /// <param name="id">the user identifier.</param>
    /// <returns>the user if found; otherwise, null.</returns>
    Task<User?> GetByIdAsync(int id);
}