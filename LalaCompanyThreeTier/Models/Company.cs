using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LalaCompanyThreeTier.Models;

[Table("company")]
public partial class Company
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("companyName")]
    [StringLength(255)]
    public string? CompanyName { get; set; }

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

    [Column("city")]
    [StringLength(255)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(255)]
    public string? State { get; set; }

    [Column("pinCode")]
    [StringLength(255)]
    public string? PinCode { get; set; }

    [Column("bankName")]
    [StringLength(255)]
    public string? BankName { get; set; }

    [Column("ifscCode")]
    [StringLength(255)]
    public string? IfscCode { get; set; }

    [Column("accNumber")]
    [StringLength(255)]
    public string? AccNumber { get; set; }

    [Column("signImagePath")]
    [StringLength(255)]
    public string? SignImagePath { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}
