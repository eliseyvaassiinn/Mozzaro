using Microsoft.AspNetCore.Mvc;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.API.Controllers;

/// <summary>
/// provides API endpoints for managing pizzas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;

    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    /// <summary>
    /// gets all pizzas.
    /// </summary>
    /// <returns>a collection of pizzas.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pizza>>> GetAll()
    {
        var pizzas = await _pizzaService.GetAllAsync();

        return Ok(pizzas);
    }

    /// <summary>
    /// gets a pizza by its identifier.
    /// </summary>
    /// <param name="id">the pizza identifier.</param>
    /// <returns>the pizza if found; otherwise, a not found response.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pizza>> GetById(int id)
    {
        var pizza = await _pizzaService.GetByIdAsync(id);

        if (pizza is null)
            return NotFound();

        return Ok(pizza);
    }

    /// <summary>
    /// creates a new pizza.
    /// </summary>
    /// <param name="pizza">the pizza to create.</param>
    /// <returns>the created pizza.</returns>
    [HttpPost]
    public async Task<ActionResult> Create(Pizza pizza)
    {
        await _pizzaService.AddAsync(pizza);

        return CreatedAtAction(
            nameof(GetById),
            new { id = pizza.Id },
            pizza);
    }

    /// <summary>
    /// updates an existing pizza.
    /// </summary>
    /// <param name="id">the pizza identifier.</param>
    /// <param name="pizza">the updated pizza data.</param>
    /// <returns>a no content response if successful.</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var existingPizza = await _pizzaService.GetByIdAsync(id);

        if (existingPizza is null)
            return NotFound();

        await _pizzaService.UpdateAsync(pizza);

        return NoContent();
    }

    /// <summary>
    /// deletes a pizza by its identifier.
    /// </summary>
    /// <param name="id">the pizza identifier.</param>
    /// <returns>a no content response if successful.</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var pizza = await _pizzaService.GetByIdAsync(id);

        if (pizza is null)
            return NotFound();

        await _pizzaService.DeleteAsync(id);

        return NoContent();
    }
}