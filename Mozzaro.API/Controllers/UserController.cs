using Microsoft.AspNetCore.Mvc;
using Mozzaro.Application.Interfaces.Services;

namespace Mozzaro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

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


public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}


public class LoginRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}