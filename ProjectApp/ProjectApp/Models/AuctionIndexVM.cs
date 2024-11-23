namespace ProjectApp.Models;

public class AuctionIndexVM
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string OwnerId { get; set; }
    public decimal StartingPrice { get; set; }
    public DateTime EndDate { get; set; }
    public int Id { get; set; }
}
