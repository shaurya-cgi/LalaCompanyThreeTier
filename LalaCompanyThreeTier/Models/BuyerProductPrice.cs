using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LalaCompanyThreeTier.Models;

[Table("buyer_product_prices")]
public partial class BuyerProductPrice
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("buyerId")]
    public int? BuyerId { get; set; }

    [Column("productId")]
    public int? ProductId { get; set; }

    [Column("rate", TypeName = "decimal(18, 2)")]
    public decimal? Rate { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BuyerId")]
    [InverseProperty("BuyerProductPrices")]
    public virtual Buyer? Buyer { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("BuyerProductPrices")]
    public virtual Product? Product { get; set; }
}
