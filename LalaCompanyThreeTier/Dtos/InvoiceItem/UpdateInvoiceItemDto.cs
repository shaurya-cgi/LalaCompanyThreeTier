namespace LalaCompanyThreeTier.Dtos.InvoiceItem;

public class UpdateInvoiceItemDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Qty { get; set; }

    public decimal Rate { get; set; }

    public decimal Amount { get; set; }

    public decimal GstRate { get; set; }

    public decimal GstAmount { get; set; }

    public decimal TotalAmount { get; set; }
}