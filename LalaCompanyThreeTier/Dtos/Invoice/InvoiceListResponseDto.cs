namespace LalaCompanyThreeTier.Dtos.Invoice
{
    public class InvoiceListResponseDto
    {
        public int Id { get; set; }

        public string InvoiceNo { get; set; } = string.Empty;

        public int? BuyerId { get; set; }

        public string BuyerName { get; set; } = string.Empty;

        public DateOnly? InvoiceDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? Status { get; set; }
    }
}