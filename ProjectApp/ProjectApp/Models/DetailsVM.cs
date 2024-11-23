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

}