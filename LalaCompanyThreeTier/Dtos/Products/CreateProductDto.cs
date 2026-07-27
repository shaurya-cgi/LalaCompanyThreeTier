namespace LalaCompanyThreeTier.Dtos.Product;

public class CreateProductDto
{
    public int CategoryId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public decimal DefaultPrice { get; set; }

    public decimal Gstrate { get; set; }
}