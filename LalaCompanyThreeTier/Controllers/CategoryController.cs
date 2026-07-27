using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Dtos.Category;
using LalaCompanyThreeTier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategory()
    {
        var categories = await _context.Categories
            .Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                CategoryName = c.CategoryName ?? string.Empty
            })
            .ToListAsync();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(new CategoryResponseDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName ?? string.Empty
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(
    int id,
    UpdateCategoryDto dto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        category.CategoryName = dto.CategoryName;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> PostCategory(
     CreateCategoryDto dto)
    {
        var category = new Category
        {
            CategoryName = dto.CategoryName
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return Ok(new CategoryResponseDto
        {
            Id = category.Id,
            CategoryName = category.CategoryName ?? string.Empty
        });
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int? id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoryExists(int? id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}
