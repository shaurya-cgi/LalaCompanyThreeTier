using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LalaCompanyThreeTier.Models;
using LalaCompanyThreeTier.Data;

[Route("api/[controller]")]
[ApiController]
public class BuyersController : ControllerBase
{
    private readonly AppDbContext _context;
    public BuyersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyer()
    {
        return await _context.Buyers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Buyer>> GetBuyer(int id)
    {
        var buyer = await _context.Buyers.FindAsync(id);

        if (buyer == null)
        {
            return NotFound();
        }

        return buyer;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBuyer(int? id, Buyer buyer)
    {
        if (id != buyer.Id)
        {
            return BadRequest();
        }

        _context.Entry(buyer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BuyerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer)
    {
        _context.Buyers.Add(buyer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBuyer", new { id = buyer.Id }, buyer);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuyer(int? id)
    {
        var buyer = await _context.Buyers.FindAsync(id);
        if (buyer == null)
        {
            return NotFound();
        }

        _context.Buyers.Remove(buyer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BuyerExists(int? id)
    {
        return _context.Buyers.Any(e => e.Id == id);
    }
}
