namespace LalaCompanyThreeTier.Dtos.Invoice
{
    public class UpdateInvoiceDto
    {
        public string InvoiceNo { get; set; } = string.Empty;

        public int BuyerId { get; set; }

        public DateOnly InvoiceDate { get; set; }

        public decimal Subtotal { get; set; }

        public decimal GstAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }
    }
}