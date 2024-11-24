using ProjectApp.Data;
using ProjectApp.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectApp.Services
{
    public class BidService : IBidRepository
    {
        private readonly AuctionDBContext _context;

        public BidService(AuctionDBContext context)
        {
            _context = context;
        }

        // Lägg till bud på auktionen
        public void PlaceBid(Bid bid)
        {
            var auction = _context.Auctions.Include(a => a.Bids).FirstOrDefault(a => a.Id == bid.AuctionId);
            if (auction == null) throw new ArgumentException("Auktionen finns inte.");

            // Kontrollera att användaren inte lägger bud på sin egen auktion
            if (auction.OwnerId == bid.UserId)
                throw new UnauthorizedAccessException("Du kan inte lägga bud på din egen auktion.");

            // Hämta det högsta budet
            var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

            // Kontrollera att budet är högre än startpriset och det aktuella högsta budet
            if (bid.Amount <= auction.StartingPrice)
                throw new ArgumentException($"Budet måste vara högre än startpriset på {auction.StartingPrice}.");
            if (bid.Amount <= (highestBid?.Amount ?? auction.StartingPrice))
                throw new ArgumentException("Budet måste vara högre än det aktuella högsta budet.");

            auction.Bids.Add(bid); // Lägg till budet till auktionens Bids
            _context.SaveChanges(); // Spara ändringar i databasen
        }

        // Hämta alla bud för en auktion
        public List<Bid> GetBidsForAuction(int auctionId)
        { 
            return _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.Amount)
                .ToList(); // Hämta alla bud för en auktion
        }
    

        public Bid GetBidDetails(int bidId)
        {
            return _context.Bids
                .Include(b => b.Auction) // Inkludera auktionen om nödvändigt
                .FirstOrDefault(b => b.Id == bidId);
        }
    }
}