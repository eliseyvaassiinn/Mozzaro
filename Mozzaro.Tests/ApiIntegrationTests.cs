using System.Net;
using System.Net.Http.Json;

namespace Mozzaro.Tests;

public class ApiIntegrationTests
{
    private readonly HttpClient _client;

    public ApiIntegrationTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5088/")
        };
    }

    [Fact]
    public async Task RegisterAndLogin_ShouldWork()
    {
        var email = $"test_{Guid.NewGuid():N}@test.com";

        var registerRequest = new
        {
            Name = "Test User",
            Email = email,
            Password = "password123"
        };

        var registerResponse = await _client.PostAsJsonAsync(
            "api/User/register",
            registerRequest);

        Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

        var registerResult =
            await registerResponse.Content.ReadFromJsonAsync<UserResponse>();

        Assert.NotNull(registerResult);
        Assert.True(registerResult.id > 0);
        Assert.Equal(email, registerResult.email);

        var loginRequest = new
        {
            Email = email,
            Password = "password123"
        };

        var loginResponse = await _client.PostAsJsonAsync(
            "api/User/login",
            loginRequest);

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginResult =
            await loginResponse.Content.ReadFromJsonAsync<UserResponse>();

        Assert.NotNull(loginResult);
        Assert.Equal(registerResult.id, loginResult.id);
        Assert.Equal(email, loginResult.email);
    }

    [Fact]
    public async Task GetUserOrders_ShouldReturnSuccessfulResponse()
    {
        var email = $"orders_{Guid.NewGuid():N}@test.com";

        var registerRequest = new
        {
            Name = "Orders Test User",
            Email = email,
            Password = "password123"
        };

        var registerResponse = await _client.PostAsJsonAsync(
            "api/User/register",
            registerRequest);

        Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

        var user =
            await registerResponse.Content.ReadFromJsonAsync<UserResponse>();

        Assert.NotNull(user);

        var ordersResponse = await _client.GetAsync(
            $"api/Order/user/{user.id}");

        Assert.Equal(HttpStatusCode.OK, ordersResponse.StatusCode);

        var orders =
            await ordersResponse.Content
                .ReadFromJsonAsync<List<OrderResponse>>();

        Assert.NotNull(orders);
    }

    [Fact]
    public async Task Register_ShouldReturnConflict_WhenEmailAlreadyExists()
    {
        var email = $"duplicate_{Guid.NewGuid():N}@test.com";

        var request = new
        {
            Name = "Test User",
            Email = email,
            Password = "password123"
        };

        var firstResponse = await _client.PostAsJsonAsync(
            "api/User/register",
            request);

        Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);

        var secondResponse = await _client.PostAsJsonAsync(
            "api/User/register",
            request);

        Assert.Equal(HttpStatusCode.Conflict, secondResponse.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenPasswordIsWrong()
    {
        var email = $"login_{Guid.NewGuid():N}@test.com";

        var registerRequest = new
        {
            Name = "Login Test User",
            Email = email,
            Password = "password123"
        };

        var registerResponse = await _client.PostAsJsonAsync(
            "api/User/register",
            registerRequest);

        Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

        var loginRequest = new
        {
            Email = email,
            Password = "wrong-password"
        };

        var loginResponse = await _client.PostAsJsonAsync(
            "api/User/login",
            loginRequest);

        Assert.Equal(
            HttpStatusCode.Unauthorized,
            loginResponse.StatusCode);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenPasswordIsTooShort()
    {
        var request = new
        {
            Name = "Test User",
            Email = $"short_{Guid.NewGuid():N}@test.com",
            Password = "123"
        };

        var response = await _client.PostAsJsonAsync(
            "api/User/register",
            request);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task Login_ShouldReturnBadRequest_WhenCredentialsAreEmpty()
    {
        var request = new
        {
            Email = "",
            Password = ""
        };

        var response = await _client.PostAsJsonAsync(
            "api/User/login",
            request);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    private class UserResponse
    {
        public int id { get; set; }

        public string name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;
    }

    private class OrderResponse
    {
        public int id { get; set; }

        public string status { get; set; } = string.Empty;

        public decimal totalPrice { get; set; }
    }
}