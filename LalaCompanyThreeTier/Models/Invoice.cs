using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LalaCompanyThreeTier.Models;

[Table("invoices")]
public partial class Invoice
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("invoiceNo")]
    [StringLength(255)]
    public string? InvoiceNo { get; set; }

    [Column("buyerId")]
    public int? BuyerId { get; set; }

    [Column("invoiceDate")]
    public DateOnly? InvoiceDate { get; set; }

    [Column("subtotal", TypeName = "decimal(18, 2)")]
    public decimal? Subtotal { get; set; }

    [Column("gstAmount", TypeName = "decimal(18, 2)")]
    public decimal? GstAmount { get; set; }

    [Column("totalAmount", TypeName = "decimal(18, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column("pdfPath")]
    [StringLength(255)]
    public string? PdfPath { get; set; }

    [Column("status")]
    [StringLength(255)]
    public string? Status { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BuyerId")]
    [InverseProperty("Invoices")]
    public virtual Buyer? Buyer { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
