using Microsoft.AspNetCore.Identity;

namespace ProjectApp.Models;

public class DetailsVM
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal StartingPrice { get; set; }
    public DateTime EndDate { get; set; }
    public List<BidVM> Bids { get; set; }
    public string OwnerId { get; set; }
    public string OwnerName { get; set; } // Lägg till fält för användarnamn

    
    // Factory-metod för att skapa DetailsVM från en Auction-entitet
    public static DetailsVM FromAuction(Auction auction, string ownerName, UserManager<AppIdentityUser> userManager)
    {
        return new DetailsVM
        {
            Id = auction.Id,
            Name = auction.Name,
            Description = auction.Description,
            OwnerId = auction.OwnerId,
            OwnerName = ownerName,
            StartingPrice = auction.StartingPrice,
            EndDate = auction.EndDate,
            Bids = auction.Bids.Select(b => new BidVM
            {
                BidAmount = b.Amount,
                UserId = b.UserId,
                Username = userManager.FindByIdAsync(b.UserId).Result.UserName, // Hämta användarnamn
                BidTime = b.BidTime
            }).ToList()
        };
    }
}