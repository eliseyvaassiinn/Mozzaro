using Microsoft.AspNetCore.Mvc;
using Mozzaro.Application.Interfaces.Services;

namespace Mozzaro.API.Controllers;

/// <summary>
/// provides API endpoints for user registration and authentication.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// registers a new user.
    /// </summary>
    /// <param name="request">the user registration data.</param>
    /// <returns>the registered user's information.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest("Имя обязательно.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest("Email обязателен.");
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Пароль обязателен.");
        }

        if (request.Password.Length < 6)
        {
            return BadRequest("Пароль должен содержать минимум 6 символов.");
        }

        var user = await _userService.RegisterAsync(
            request.Name,
            request.Email,
            request.Password);

        if (user == null)
        {
            return Conflict("Пользователь с таким email уже существует.");
        }

        return Ok(new
        {
            id = user.Id,
            name = user.Name,
            email = user.Email
        });
    }

    /// <summary>
    /// authenticates a user.
    /// </summary>
    /// <param name="request">the login credentials.</param>
    /// <returns>the authenticated user's information.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Введите email и пароль.");
        }

        var user = await _userService.LoginAsync(
            request.Email,
            request.Password);

        if (user == null)
        {
            return Unauthorized("Неверный email или пароль.");
        }

        return Ok(new
        {
            id = user.Id,
            name = user.Name,
            email = user.Email
        });
    }
}

/// <summary>
/// represents the data required to register a user.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// gets or sets the user's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the user's password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// represents the credentials required to log in.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// gets or sets the user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the user's password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}