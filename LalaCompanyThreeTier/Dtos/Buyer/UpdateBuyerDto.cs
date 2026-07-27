using System.ComponentModel.DataAnnotations;


namespace LalaCompanyThreeTier.Dtos.Buyer
{
    public class UpdateBuyerDto
    {
        [Required]
        [StringLength(255)]
        public string PartyName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Gstin { get; set; }

        [StringLength(255)]
        public string? Mobile { get; set; }

        [StringLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        public string? BillingAddress { get; set; }

        [StringLength(255)]
        public string? State { get; set; }

        [StringLength(255)]
        public string? City { get; set; }

        [StringLength(255)]
        public string? PinCode { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}