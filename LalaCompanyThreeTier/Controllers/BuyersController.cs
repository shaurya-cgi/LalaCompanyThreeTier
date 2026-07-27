using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LalaCompanyThreeTier.Models;
using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Dtos.Buyer;
using System.Net;

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
    [HttpGet]
    public async Task<IActionResult> GetBuyer()
    {
        var buyers = await _context.Buyers
            .Select(b => new
            {
                b.Id,
                b.PartyName,
                b.Gstin,
                b.Mobile,
                b.Email,
                b.City,
                b.State,
                b.PinCode
            })
            .ToListAsync();

        return Ok(buyers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuyerResponseDto>> GetBuyer(int id)
    {
        var buyer = await _context.Buyers.FindAsync(id);

        if (buyer == null)
        {
            return NotFound();
        }

        var dto = new BuyerResponseDto
        {
            Id = buyer.Id,
            PartyName = buyer.PartyName,
            Gstin = buyer.Gstin,
            Mobile = buyer.Mobile,
            Email = buyer.Email,
            State = buyer.State,
            City = buyer.City,
            PinCode = buyer.PinCode
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBuyer(int? id, UpdateBuyerDto dto)
    {
        var buyer = new Buyer
        {
            PartyName = dto.PartyName,
            Gstin = dto.Gstin,
            Mobile = dto.Mobile,
            Email = dto.Email,
            BillingAddress = dto.BillingAddress,
            State = dto.State,
            City = dto.City,
            PinCode = dto.PinCode,
            UpdatedAt = dto.UpdatedAt
        };
        var _ = await _context.Buyers.FindAsync(id);

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
    public async Task<ActionResult<Buyer>> PostBuyer(CreateBuyerDto createBuyerDto)
    {
        var buyer = new Buyer
        {
            PartyName = createBuyerDto.PartyName,
            Gstin = createBuyerDto.Gstin,
            Mobile = createBuyerDto.Mobile,
            Email = createBuyerDto.Email,
            BillingAddress = createBuyerDto.BillingAddress,
            State = createBuyerDto.State,
            City = createBuyerDto.City,
            PinCode = createBuyerDto.PinCode,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

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
