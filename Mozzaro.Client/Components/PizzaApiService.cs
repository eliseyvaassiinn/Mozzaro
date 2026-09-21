using System.Net.Http.Json;
using Mozzaro.Domain.Entities;

namespace Mozzaro.Client.Services;

public class PizzaApiService
{
    private readonly HttpClient _httpClient;

    public PizzaApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Pizza>> GetPizzasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Pizza>>("api/Pizza")
               ?? new List<Pizza>();
    }

    public async Task<Pizza?> GetPizzaByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Pizza>($"api/Pizza/{id}");
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Category>>("api/Category")
               ?? new List<Category>();
    }
}