using LalaCompanyThreeTier.Data;
using LalaCompanyThreeTier.Dtos.Company;
using LalaCompanyThreeTier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly AppDbContext _context;

    public CompanyController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<CompanyResponseDto>> GetCompany()
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync();

        if (company == null)
        {
            return NotFound();
        }

        return Ok(new CompanyResponseDto
        {
            Id = company.Id,
            CompanyName = company.CompanyName,
            Gstin = company.Gstin,
            Mobile = company.Mobile,
            Email = company.Email,
            BillingAddress = company.BillingAddress,
            City = company.City,
            State = company.State,
            PinCode = company.PinCode,
            BankName = company.BankName,
            IfscCode = company.IfscCode,
            AccNumber = company.AccNumber,
            SignImagePath = company.SignImagePath
        });
    }

    [HttpPost]
    public async Task<ActionResult<CompanyResponseDto>> PostCompany(
        CreateCompanyDto dto)
    {
        var company = new Company
        {
            CompanyName = dto.CompanyName,
            Gstin = dto.Gstin,
            Mobile = dto.Mobile,
            Email = dto.Email,
            BillingAddress = dto.BillingAddress,
            City = dto.City,
            State = dto.State,
            PinCode = dto.PinCode,
            BankName = dto.BankName,
            IfscCode = dto.IfscCode,
            AccNumber = dto.AccNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Companies.Add(company);

        await _context.SaveChangesAsync();

        return Ok(new CompanyResponseDto
        {
            Id = company.Id,
            CompanyName = company.CompanyName,
            Gstin = company.Gstin,
            Mobile = company.Mobile,
            Email = company.Email,
            BillingAddress = company.BillingAddress,
            City = company.City,
            State = company.State,
            PinCode = company.PinCode,
            BankName = company.BankName,
            IfscCode = company.IfscCode,
            AccNumber = company.AccNumber,
            SignImagePath = company.SignImagePath
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(
        int id,
        UpdateCompanyDto dto)
    {
        var company = await _context.Companies
            .FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        company.CompanyName = dto.CompanyName;
        company.Gstin = dto.Gstin;
        company.Mobile = dto.Mobile;
        company.Email = dto.Email;
        company.BillingAddress = dto.BillingAddress;
        company.City = dto.City;
        company.State = dto.State;
        company.PinCode = dto.PinCode;
        company.BankName = dto.BankName;
        company.IfscCode = dto.IfscCode;
        company.AccNumber = dto.AccNumber;
        company.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpPost("{id}/signature")]
    public async Task<IActionResult> UploadSignature(
    int id,
    IFormFile file)
    {
        var company = await _context.Companies
            .FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        var folder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads"
        );

        Directory.CreateDirectory(folder);

        var filePath = Path.Combine(
            folder,
            "signature.jpg"
        );

        using (var stream = new FileStream(
            filePath,
            FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        company.SignImagePath =
            "/uploads/signature.jpg";

        await _context.SaveChangesAsync();

        return Ok(company.SignImagePath);
    }
}