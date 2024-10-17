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

    public void UpdateAuctionDescription(int auctionId, string description)
    {
        var auction = _context.Auctions.Find(auctionId);
        if (auction != null)
        {
            auction.Description = description;

            // Validera auktionen efter uppdatering av beskrivningen
            ValidateAuction(auction);
        
            _context.SaveChanges(); // Spara synkront
        }
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