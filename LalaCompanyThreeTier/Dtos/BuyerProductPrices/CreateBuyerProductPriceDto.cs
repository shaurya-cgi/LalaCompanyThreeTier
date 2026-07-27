namespace LalaCompanyThreeTier.Dtos.BuyerProductPrice;

public class CreateBuyerProductPriceDto
{
    public int BuyerId { get; set; }

    public int ProductId { get; set; }

    public decimal Rate { get; set; }
}