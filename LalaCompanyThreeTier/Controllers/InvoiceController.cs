using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Dtos.Invoice;
using LalaCompanyThreeTier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public InvoiceController(AppDbContext context)
    {
        _dbContext = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InvoiceListResponseDto>>> GetInvoices()
    {
        var invoices = await _dbContext.Invoices
            .Include(i => i.Buyer)
            .Select(i => new InvoiceListResponseDto
            {
                Id = i.Id,
                InvoiceNo = i.InvoiceNo ?? string.Empty,
                BuyerId = i.BuyerId,
                BuyerName = i.Buyer != null
                    ? i.Buyer.PartyName ?? string.Empty
                    : string.Empty,
                InvoiceDate = i.InvoiceDate,
                TotalAmount = i.TotalAmount,
                Status = i.Status
            })
            .OrderByDescending(i => i.Id)
            .ToListAsync();

        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InvoiceResponseDto>> GetInvoiceById(int id)
    {
        var invoice = await _dbContext.Invoices
            .Include(i => i.Buyer)
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (invoice == null)
        {
            return NotFound();
        }

        return Ok(invoice);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceResponseDto>> PostInvoice(CreateInvoiceDto dto)
    {
        var invoice = new Invoice
        {
            InvoiceNo = dto.InvoiceNo,
            BuyerId = dto.BuyerId,
            InvoiceDate = dto.InvoiceDate,
            Subtotal = dto.Subtotal,
            GstAmount = dto.GstAmount,
            TotalAmount = dto.TotalAmount,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _dbContext.Invoices.Add(invoice);

        await _dbContext.SaveChangesAsync();

        var buyer = await _dbContext.Buyers
            .FindAsync(invoice.BuyerId);

        return Ok(new InvoiceResponseDto
        {
            Id = invoice.Id,
            InvoiceNo = invoice.InvoiceNo ?? string.Empty,
            BuyerId = invoice.BuyerId,
            BuyerName = buyer?.PartyName ?? string.Empty,
            InvoiceDate = invoice.InvoiceDate,
            Subtotal = invoice.Subtotal,
            GstAmount = invoice.GstAmount,
            TotalAmount = invoice.TotalAmount,
            Status = invoice.Status
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutInvoice(int id, UpdateInvoiceDto dto)
    {
        var invoice = await _dbContext.Invoices
            .FindAsync(id);

        if (invoice == null)
        {
            return NotFound();
        }

        invoice.InvoiceNo = dto.InvoiceNo;
        invoice.BuyerId = dto.BuyerId;
        invoice.InvoiceDate = dto.InvoiceDate;
        invoice.Subtotal = dto.Subtotal;
        invoice.GstAmount = dto.GstAmount;
        invoice.TotalAmount = dto.TotalAmount;
        invoice.Status = dto.Status;
        invoice.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}