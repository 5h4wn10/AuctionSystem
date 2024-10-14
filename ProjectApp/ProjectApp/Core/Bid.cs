namespace ProjectApp.Models;

public class Bid
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime BidTime { get; set; }
    public int AuctionId { get; set; }
    public Auction Auction { get; set; }
}
