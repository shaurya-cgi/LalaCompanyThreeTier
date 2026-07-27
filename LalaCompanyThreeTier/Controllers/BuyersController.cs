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
                b.BillingAddress,
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
    public async Task<IActionResult> PutBuyer(int id, UpdateBuyerDto dto)
    {
        var buyer = await _context.Buyers.FindAsync(id);

        if (buyer == null)
        {
            return NotFound();
        }

        buyer.PartyName = dto.PartyName;
        buyer.Gstin = dto.Gstin;
        buyer.Mobile = dto.Mobile;
        buyer.Email = dto.Email;
        buyer.BillingAddress = dto.BillingAddress;
        buyer.State = dto.State;
        buyer.City = dto.City;
        buyer.PinCode = dto.PinCode;
        buyer.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

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
