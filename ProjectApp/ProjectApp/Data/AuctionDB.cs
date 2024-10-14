using System.ComponentModel.DataAnnotations;
using ProjectApp.Models;

namespace ProjectApp.Data;

public class AuctionDB
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Namnet på auktionen är obligatoriskt.")]
    [StringLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Beskrivning är obligatorisk.")]
    public string Description { get; set; }

    public string OwnerId { get; set; } // Användarens ID som äger auktionen

    [Required(ErrorMessage = "Utgångspriset är obligatoriskt.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Utgångspriset måste vara större än 0.")]
    public decimal StartingPrice { get; set; }

    [Required(ErrorMessage = "Slutdatum är obligatoriskt.")]
    public DateTime EndDate { get; set; }

    // Navigation property för bud
    public virtual ICollection<Bid> Bids { get; set; }
}