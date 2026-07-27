using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Dtos.InvoiceItem;
using LalaCompanyThreeTier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class InvoiceItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public InvoiceItemsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceItemResponseDto>>> GetInvoiceItems()
    {
        var items = await _context.InvoiceItems
            .Select(i => new InvoiceItemResponseDto
            {
                Id = i.Id,
                InvoiceId = i.InvoiceId,
                ProductId = i.ProductId,
                ProductName = i.ProductName ?? string.Empty,
                Qty = i.Qty,
                Rate = i.Rate,
                Amount = i.Amount,
                GstRate = i.GstRate,
                GstAmount = i.GstAmount,
                TotalAmount = i.TotalAmount
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceItemResponseDto>> GetInvoiceItem(int id)
    {
        var item = await _context.InvoiceItems.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(new InvoiceItemResponseDto
        {
            Id = item.Id,
            InvoiceId = item.InvoiceId,
            ProductId = item.ProductId,
            ProductName = item.ProductName ?? string.Empty,
            Qty = item.Qty,
            Rate = item.Rate,
            Amount = item.Amount,
            GstRate = item.GstRate,
            GstAmount = item.GstAmount,
            TotalAmount = item.TotalAmount
        });
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceItemResponseDto>> PostInvoiceItem(
        CreateInvoiceItemDto dto)
    {
        var item = new InvoiceItem
        {
            InvoiceId = dto.InvoiceId,
            ProductId = dto.ProductId,
            ProductName = dto.ProductName,
            Qty = dto.Qty,
            Rate = dto.Rate,
            Amount = dto.Amount,
            GstRate = dto.GstRate,
            GstAmount = dto.GstAmount,
            TotalAmount = dto.TotalAmount,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.InvoiceItems.Add(item);

        await _context.SaveChangesAsync();

        return Ok(new InvoiceItemResponseDto
        {
            Id = item.Id,
            InvoiceId = item.InvoiceId,
            ProductId = item.ProductId,
            ProductName = item.ProductName ?? string.Empty,
            Qty = item.Qty,
            Rate = item.Rate,
            Amount = item.Amount,
            GstRate = item.GstRate,
            GstAmount = item.GstAmount,
            TotalAmount = item.TotalAmount
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutInvoiceItem(
        int id,
        UpdateInvoiceItemDto dto)
    {
        var item = await _context.InvoiceItems.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        item.ProductId = dto.ProductId;
        item.ProductName = dto.ProductName;
        item.Qty = dto.Qty;
        item.Rate = dto.Rate;
        item.Amount = dto.Amount;
        item.GstRate = dto.GstRate;
        item.GstAmount = dto.GstAmount;
        item.TotalAmount = dto.TotalAmount;
        item.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoiceItem(int id)
    {
        var item = await _context.InvoiceItems.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        _context.InvoiceItems.Remove(item);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}