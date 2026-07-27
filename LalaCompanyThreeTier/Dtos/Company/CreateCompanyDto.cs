namespace LalaCompanyThreeTier.Dtos.Company;

public class CreateCompanyDto
{
    public string? CompanyName { get; set; }

    public string? Gstin { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? BillingAddress { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PinCode { get; set; }

    public string? BankName { get; set; }

    public string? IfscCode { get; set; }

    public string? AccNumber { get; set; }
}