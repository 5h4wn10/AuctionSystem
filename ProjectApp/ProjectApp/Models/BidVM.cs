namespace ProjectApp.Models;

public class BidVM
{
    public int AuctionId { get; set; }
    public decimal BidAmount { get; set; }
    public string UserId { get; set; }
    public DateTime BidTime { get; set; }
}