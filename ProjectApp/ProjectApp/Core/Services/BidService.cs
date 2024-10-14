using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Services;
using Microsoft.EntityFrameworkCore; // Kom ihåg att importera Entity Framework-namnrymden

public class BidRepository : IBidRepository
{
    private readonly AuctionDBContext _context;

    public BidRepository(AuctionDBContext context)
    {
        _context = context;
    }

    public List<Bid> GetBidsForAuction(int auctionId)
    {
        return _context.Bids
            .Where(b => b.AuctionId == auctionId)
            .OrderByDescending(b => b.Amount)
            .ToList(); // Hämta alla bud för en auktion
    }

    public void PlaceBid(Bid bid)
    {
        // Kontrollera att budet är högre än tidigare bud
        var highestBid = _context.Bids
            .Where(b => b.AuctionId == bid.AuctionId)
            .OrderByDescending(b => b.Amount)
            .FirstOrDefault();

        if (highestBid != null && bid.Amount <= highestBid.Amount)
        {
            throw new InvalidOperationException("Bud måste vara högre än det nuvarande högsta budet.");
        }

        _context.Bids.Add(bid);
        _context.SaveChanges(); // Spara synkront
    }

    public Bid GetBidDetails(int bidId)
    {
        return _context.Bids
            .Include(b => b.Auction) // Inkludera auktionen om nödvändigt
            .FirstOrDefault(b => b.Id == bidId);
    }
}