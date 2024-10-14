using System.ComponentModel.DataAnnotations;
using ProjectApp.Models;

namespace ProjectApp.Data;

public class BidDB
{
    public int Id { get; set; }

    public string UserId { get; set; } // Användarens ID som lägger budet

    [Required(ErrorMessage = "Budets storlek är obligatoriskt.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Budet måste vara större än 0.")]
    public decimal Amount { get; set; }

    public DateTime BidTime { get; set; } // Tiden då budet lades

    public int AuctionId { get; set; } // FK till auktionen

    // Navigation property till auktionen
    public virtual Auction Auction { get; set; }
}