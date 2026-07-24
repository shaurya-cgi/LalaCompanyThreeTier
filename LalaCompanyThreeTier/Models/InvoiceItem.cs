using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LalaCompanyThreeTier.Models;

[Table("invoice_items")]
public partial class InvoiceItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("invoiceId")]
    public int? InvoiceId { get; set; }

    [Column("productId")]
    public int? ProductId { get; set; }

    [Column("productName")]
    [StringLength(255)]
    public string? ProductName { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }

    [Column("rate", TypeName = "decimal(18, 2)")]
    public decimal? Rate { get; set; }

    [Column("amount", TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [Column("gstRate", TypeName = "decimal(18, 2)")]
    public decimal? GstRate { get; set; }

    [Column("gstAmount", TypeName = "decimal(18, 2)")]
    public decimal? GstAmount { get; set; }

    [Column("totalAmount", TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceItems")]
    public virtual Invoice? Invoice { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("InvoiceItems")]
    public virtual Product? Product { get; set; }
}
