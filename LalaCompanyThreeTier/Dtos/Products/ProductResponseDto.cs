namespace LalaCompanyThreeTier.Dtos.Product;

public class ProductResponseDto
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public decimal? DefaultPrice { get; set; }

    public decimal? Gstrate { get; set; }

    public List<BuyerPriceDto> BuyerPrices { get; set; } = [];
}