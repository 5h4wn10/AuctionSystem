using ProjectApp.Models;

namespace ProjectApp.Services;

public interface IAuctionRepository
{
    List<Auction> GetActiveAuctions(); // Synkron metod för att hämta aktiva auktioner
    Auction GetAuctionDetails(int auctionId); // Synkron metod för att hämta detaljer för en auktion
    void CreateAuction(Auction auction); // Synkron metod för att skapa en auktion
    void EditAuctionDescription(int auctionId, string description);

    List<Auction> GetAuctionsByUser(string userId);

    void PlaceBid(Bid bid);


}
