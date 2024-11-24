using ProjectApp.Models;

namespace ProjectApp.Services;

public interface IBidRepository
{
    List<Bid> GetBidsForAuction(int auctionId); // Synkron metod för att hämta bud för en auktion
    void PlaceBid(Bid bid); // Synkron metod för att lägga ett bud
    Bid GetBidDetails(int bidId); // Synkron metod för att hämta detaljer om ett bud
    
}
