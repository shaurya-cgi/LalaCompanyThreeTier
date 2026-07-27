using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Dtos.BuyerProductPrice;
using LalaCompanyThreeTier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BuyerProductPricesController : ControllerBase
{
    private readonly AppDbContext _context;

    public BuyerProductPricesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BuyerProductPriceResponseDto>>> GetAll()
    {
        var prices = await _context.BuyerProductPrices
            .Include(x => x.Buyer)
            .Include(x => x.Product)
            .Select(x => new BuyerProductPriceResponseDto
            {
                Id = x.Id,
                BuyerId = x.BuyerId,
                BuyerName = x.Buyer != null
                    ? x.Buyer.PartyName ?? string.Empty
                    : string.Empty,
                ProductId = x.ProductId,
                ProductName = x.Product != null
                    ? x.Product.ModelName ?? string.Empty
                    : string.Empty,
                Rate = x.Rate
            })
            .ToListAsync();

        return Ok(prices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BuyerProductPriceResponseDto>> GetById(int id)
    {
        var price = await _context.BuyerProductPrices
            .Include(x => x.Buyer)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (price == null)
        {
            return NotFound();
        }

        return Ok(new BuyerProductPriceResponseDto
        {
            Id = price.Id,
            BuyerId = price.BuyerId,
            BuyerName = price.Buyer?.PartyName ?? string.Empty,
            ProductId = price.ProductId,
            ProductName = price.Product?.ModelName ?? string.Empty,
            Rate = price.Rate
        });
    }

    [HttpGet("buyer/{buyerId}")]
    public async Task<ActionResult<IEnumerable<BuyerProductPriceResponseDto>>> GetByBuyer(int buyerId)
    {
        var prices = await _context.BuyerProductPrices
            .Include(x => x.Buyer)
            .Include(x => x.Product)
            .Where(x => x.BuyerId == buyerId)
            .Select(x => new BuyerProductPriceResponseDto
            {
                Id = x.Id,
                BuyerId = x.BuyerId,
                BuyerName = x.Buyer!.PartyName!,
                ProductId = x.ProductId,
                ProductName = x.Product!.ModelName!,
                Rate = x.Rate
            })
            .ToListAsync();

        return Ok(prices);
    }

    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<BuyerProductPriceResponseDto>>> GetByProduct(int productId)
    {
        var prices = await _context.BuyerProductPrices
            .Include(x => x.Buyer)
            .Include(x => x.Product)
            .Where(x => x.ProductId == productId)
            .Select(x => new BuyerProductPriceResponseDto
            {
                Id = x.Id,
                BuyerId = x.BuyerId,
                BuyerName = x.Buyer!.PartyName!,
                ProductId = x.ProductId,
                ProductName = x.Product!.ModelName!,
                Rate = x.Rate
            })
            .ToListAsync();

        return Ok(prices);
    }

    [HttpPost]
    public async Task<ActionResult<BuyerProductPriceResponseDto>> Post(
        CreateBuyerProductPriceDto dto)
    {
        var existing = await _context.BuyerProductPrices
            .FirstOrDefaultAsync(x =>
                x.BuyerId == dto.BuyerId &&
                x.ProductId == dto.ProductId);

        if (existing != null)
        {
            return BadRequest("Price already exists.");
        }

        var entity = new BuyerProductPrice
        {
            BuyerId = dto.BuyerId,
            ProductId = dto.ProductId,
            Rate = dto.Rate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.BuyerProductPrices.Add(entity);

        await _context.SaveChangesAsync();

        return Ok(new BuyerProductPriceResponseDto
        {
            Id = entity.Id,
            BuyerId = entity.BuyerId,
            ProductId = entity.ProductId,
            Rate = entity.Rate
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(
        int id,
        UpdateBuyerProductPriceDto dto)
    {
        var entity = await _context.BuyerProductPrices
            .FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        entity.BuyerId = dto.BuyerId;
        entity.ProductId = dto.ProductId;
        entity.Rate = dto.Rate;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.BuyerProductPrices
            .FindAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        _context.BuyerProductPrices.Remove(entity);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}