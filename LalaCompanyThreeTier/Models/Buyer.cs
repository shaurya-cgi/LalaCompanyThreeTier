using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LalaCompanyThreeTier.Models;

[Table("buyers")]
public partial class Buyer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("partyName")]
    [StringLength(255)]
    public string? PartyName { get; set; }

    [Column("gstin")]
    [StringLength(255)]
    public string? Gstin { get; set; }

    [Column("mobile")]
    [StringLength(255)]
    public string? Mobile { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("billingAddress", TypeName = "text")]
    public string? BillingAddress { get; set; }

    [Column("state")]
    [StringLength(255)]
    public string? State { get; set; }

    [Column("city")]
    [StringLength(255)]
    public string? City { get; set; }

    [Column("pinCode")]
    [StringLength(255)]
    public string? PinCode { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Buyer")]
    public virtual ICollection<BuyerProductPrice> BuyerProductPrices { get; set; } = new List<BuyerProductPrice>();

    [InverseProperty("Buyer")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
