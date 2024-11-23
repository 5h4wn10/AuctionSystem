using ProjectApp.Data;
using ProjectApp.Models;
using Microsoft.EntityFrameworkCore; // Kom ihåg att importera Entity Framework-namnrymden

namespace ProjectApp.Services;

public class AuctionRepository : IAuctionRepository
{
    private readonly AuctionDBContext _context;

    public AuctionRepository(AuctionDBContext context)
    {
        _context = context;
    }

    public List<Auction> GetActiveAuctions()
    {
        return _context.Auctions
            .Where(a => a.EndDate > DateTime.Now)
            .OrderBy(a => a.EndDate)
            .ToList();
    }
    
    public List<Auction> GetAuctionsByUser(string userId)
    {
        return _context.Auctions
            .Where(a => a.OwnerId == userId && a.EndDate > DateTime.Now) // Endast pågående auktioner
            .OrderBy(a => a.EndDate) // Sortera efter slutdatum
            .ToList();
    }

    public Auction GetAuctionDetails(int auctionId)
    {
        return _context.Auctions
            .Include(a => a.Bids) // Om du vill inkludera buden i detaljerna
            .FirstOrDefault(a => a.Id == auctionId);
    }
    

    public void CreateAuction(Auction auction)
    {
        ValidateAuction(auction);
        _context.Auctions.Add(auction);
        _context.SaveChanges(); // Spara synkront
    }

    
    
    public void EditAuctionDescription(int auctionId, string newDescription)
    {
        var auction = _context.Auctions.Find(auctionId);
        if (auction == null)
        {
            throw new ArgumentException("Auktionen hittades inte.");
        }

        /*if (auction.OwnerId != userId)
        {
            throw new UnauthorizedAccessException("Du har inte behörighet att ändra denna auktion.");
        }*/

        auction.Description = newDescription;

        ValidateAuction(auction);
        _context.SaveChanges(); // Spara synkront
    }
    
    
    public void PlaceBid(Bid bid)
    {
        var auction = _context.Auctions.Include(a => a.Bids).FirstOrDefault(a => a.Id == bid.AuctionId);
        if (auction == null) throw new ArgumentException("Auktionen finns inte.");
        
        // Hämta det högsta budet
        var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault(); 
        // Kontrollera att budet är högre än startpriset (eller det nuvarande högsta budet)
        if (bid.Amount <= auction.StartingPrice) throw new ArgumentException($"Budet måste vara högre än startpriset på {auction.StartingPrice}."); 
        // Kontrollera att budet är högre än det högsta budet
        if (bid.Amount <= (highestBid?.Amount ?? auction.StartingPrice)) throw new ArgumentException("Budet måste vara högre än det aktuella högsta budet.");

        auction.Bids.Add(bid); // Lägg till budet till auktionens Bids
        _context.SaveChanges(); // Spara ändringarna
    }

    
    
    
    public void ValidateAuction(Auction auction)
    {
        if (string.IsNullOrWhiteSpace(auction.Description)) 
            throw new ArgumentException("Beskrivningen får inte vara tom.");
        
        if (string.IsNullOrWhiteSpace(auction.Name)) 
            throw new ArgumentException("Namnet på auktionen får inte vara tomt.");
        
        if (auction.StartingPrice <= 0) 
            throw new ArgumentException("Utgångspriset måste vara större än 0.");
        
        if (auction.EndDate <= DateTime.Now)
            throw new ArgumentException("Slutdatum måste vara i framtiden.");   
    }
}