using Microsoft.AspNetCore.Mvc;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;

    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pizza>>> GetAll()
    {
        var pizzas = await _pizzaService.GetAllAsync();

        return Ok(pizzas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pizza>> GetById(int id)
    {
        var pizza = await _pizzaService.GetByIdAsync(id);

        if (pizza is null)
            return NotFound();

        return Ok(pizza);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Pizza pizza)
    {
        await _pizzaService.AddAsync(pizza);

        return CreatedAtAction(
            nameof(GetById),
            new { id = pizza.Id },
            pizza);
    }

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