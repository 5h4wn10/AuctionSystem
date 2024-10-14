namespace ProjectApp.Models;

public class Auction
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string OwnerId { get; set; }
    public decimal StartingPrice { get; set; }
    public DateTime EndDate { get; set; }
    public virtual ICollection<Bid> Bids { get; set; }
}