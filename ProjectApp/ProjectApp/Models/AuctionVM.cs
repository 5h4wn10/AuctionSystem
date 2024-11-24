namespace ProjectApp.Models;

public class AuctionVM
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    
    public decimal StartingPrice { get; set; }
    public DateTime EndDate { get; set; }
    public int Id { get; set; }
    public List<BidVM> Bids { get; set; }
    
    // Lägg till FromAuction-metoden
    public static AuctionVM FromAuction(Auction auction, string ownerName)
    {
        return new AuctionVM
        {
            Id = auction.Id,
            Name = auction.Name,
            Description = auction.Description,
            OwnerId = auction.OwnerId,
            OwnerName = ownerName,
            StartingPrice = auction.StartingPrice,
            EndDate = auction.EndDate,
            Bids = auction.Bids?.Select(b => new BidVM
            {
                BidAmount = b.Amount,
                UserId = b.UserId,
                BidTime = b.BidTime
            }).ToList() ?? new List<BidVM>()
        };
    }
}
