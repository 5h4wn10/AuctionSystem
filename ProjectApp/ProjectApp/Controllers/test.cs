if (auction == null) throw new ArgumentException("Auktionen finns inte.");
        
// Hämta det högsta budet
var highestBid = auction.Bids.OrderByDescending(b => b.Amount).FirstOrDefault(); 
// Kontrollera att budet är högre än startpriset (eller det nuvarande högsta budet)
if (bid.Amount <= auction.StartingPrice) throw new ArgumentException($"Budet måste vara högre än startpriset på {auction.StartingPrice}."); 
// Kontrollera att budet är högre än det högsta budet
if (bid.Amount <= (highestBid?.Amount ?? auction.StartingPrice)) throw new ArgumentException("Budet måste vara högre än det aktuella högsta budet.");





@model ProjectApp.Models.DetailsVM

    @{
    ViewData["Title"] = "Auction Details";
}

<h1>@Model.Name</h1>
    <p>@Model.Description</p>
    <p>Owner: @Model.OwnerName</p>
    <p>Starting Price: @Model.StartingPrice Kr</p>
    <p>End Date: @Model.EndDate</p>

    <h2>Bids</h2>
    <table class="table">
    <thead>
    <tr>
    <th>Bid Amount</th>
    <th>Bidder</th>
    <th>Bid Time</th>
    </tr>
    </thead>
    <tbody>
    @foreach (var bid in Model.Bids.OrderByDescending(b => b.BidAmount)) // Sortera buden så det högsta kommer först
{
    <tr>
        <td>@bid.BidAmount</td>
        <td>@bid.UserId</td>
        <td>@bid.BidTime</td>
        </tr>
}
</tbody>
    </table>

    <form> <div class="form-group">
    <label for="bidAmount">Your Bid Amount</label>
    <input type="number" name="bidAmount" class="form-control" min="0.01" step="0.01" required />
    </div>
    <button type="submit" class="btn btn-primary">Place Bid</button>
    </form>



    <a asp-action="Index">Back to Your Auctions</a>     <a asp-action="ActiveAuctions">View All Active Auctions</a>