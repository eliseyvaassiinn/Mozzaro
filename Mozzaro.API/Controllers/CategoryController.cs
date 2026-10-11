using Microsoft.AspNetCore.Mvc;
using Mozzaro.Application.Interfaces.Services;
using Mozzaro.Domain.Entities;

namespace Mozzaro.API.Controllers;

/// <summary>
/// provides API endpoints for managing pizza categories.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// gets all pizza categories.
    /// </summary>
    /// <returns>a list of categories.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories);
    }

    /// <summary>
    /// gets a category by its identifier.
    /// </summary>
    /// <param name="id">the category identifier.</param>
    /// <returns>the category if found; otherwise, a not found response.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    /// <summary>
    /// creates a new category.
    /// </summary>
    /// <param name="category">the category to create.</param>
    /// <returns>the created category.</returns>
    [HttpPost]
    public async Task<ActionResult> Create(Category category)
    {
        await _categoryService.AddAsync(category);

        return CreatedAtAction(
            nameof(GetById),
            new { id = category.Id },
            category);
    }

    /// <summary>
    /// updates an existing category.
    /// </summary>
    /// <param name="id">the category identifier.</param>
    /// <param name="category">the updated category data.</param>
    /// <returns>a no content response if successful.</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest();

        var existingCategory = await _categoryService.GetByIdAsync(id);

        if (existingCategory is null)
            return NotFound();

        await _categoryService.UpdateAsync(category);

        return NoContent();
    }

    /// <summary>
    /// deletes a category by its identifier.
    /// </summary>
    /// <param name="id">the category identifier.</param>
    /// <returns>a no content response if successful.</returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        await _categoryService.DeleteAsync(id);

        return NoContent();
    }
}