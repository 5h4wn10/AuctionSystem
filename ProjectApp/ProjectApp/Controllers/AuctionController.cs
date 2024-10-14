using ProjectApp.Models;
using ProjectApp.Services;

namespace ProjectApp.Controllers;

using Microsoft.AspNetCore.Mvc;

public class AuctionController : Controller
{
    private readonly IAuctionRepository _auctionService;

    public AuctionController(IAuctionRepository auctionService)
    {
        _auctionService = auctionService;
    }

    public IActionResult Index()
    {
        var auctions = _auctionService.GetActiveAuctions(); // Anropa synkron metod
        return View(auctions);
    }

    public IActionResult Details(int id)
    {
        var auction = _auctionService.GetAuctionDetails(id); // Anropa synkron metod
        if (auction == null)
        {
            return NotFound(); // Returnera 404 om auktionen inte hittas
        }
        return View(auction);
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // För att skydda mot CSRF
    public IActionResult Create(Auction auction)
    {
        if (!ModelState.IsValid)
        {
            return View(auction); // Om modellen inte är giltig, returnera till vyn
        }

        _auctionService.CreateAuction(auction); // Anropa synkron metod
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken] // För att skydda mot CSRF
    public IActionResult UpdateDescription(int id, string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            ModelState.AddModelError("Description", "Beskrivning får inte vara tom."); // Validera beskrivningen
            return RedirectToAction("Details", new { id }); // Returnera tillbaka till detaljer om det är ogiltigt
        }

        _auctionService.UpdateAuctionDescription(id, description); // Anropa synkron metod
        return RedirectToAction("Details", new { id });
    }
}