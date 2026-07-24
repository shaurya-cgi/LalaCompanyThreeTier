using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LalaCompanyThreeTier.Models;

[Table("products")]
public partial class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("categoryId")]
    public int? CategoryId { get; set; }

    [Column("modelName")]
    [StringLength(255)]
    public string? ModelName { get; set; }

    [Column("defaultPrice", TypeName = "decimal(18, 2)")]
    public decimal? DefaultPrice { get; set; }

    [Column("GSTRate", TypeName = "decimal(18, 2)")]
    public decimal? Gstrate { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<BuyerProductPrice> BuyerProductPrices { get; set; } = new List<BuyerProductPrice>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
