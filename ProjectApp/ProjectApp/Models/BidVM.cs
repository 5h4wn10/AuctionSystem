namespace ProjectApp.Models;

public class BidVM
{
    public int AuctionId { get; set; }
    public decimal BidAmount { get; set; }
    public DateTime BidTime { get; set; }
    
    public decimal HighestBidAmount { get; set; }
    public decimal StartingPrice { get; set; }
    
    public string UserId { get; set; }
    public string Username { get; set; } // Lägg till fält för användarnamn
    
    
    public static BidVM FromAuction(Auction auction, string username)
    {
        var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

        return new BidVM
        {
            AuctionId = auction.Id,
            HighestBidAmount = highestBid?.Amount ?? auction.StartingPrice,
            StartingPrice = auction.StartingPrice,
            UserId = auction.OwnerId,
            Username = username
        };
    }

    public static BidVM FromBid(Bid bid)
    {
        return new BidVM
        {
            AuctionId = bid.AuctionId,
            HighestBidAmount = bid.Amount,
            UserId = bid.UserId
        };
    }
}