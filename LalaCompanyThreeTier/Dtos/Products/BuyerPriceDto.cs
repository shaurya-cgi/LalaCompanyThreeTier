namespace LalaCompanyThreeTier.Dtos.Product;

public class BuyerPriceDto
{
    public int? BuyerId { get; set; }

    public string BuyerName { get; set; } = string.Empty;

    public decimal? Rate { get; set; }
}