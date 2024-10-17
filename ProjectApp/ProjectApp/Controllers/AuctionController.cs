using System.Security.Claims;
using ProjectApp.Models;
using ProjectApp.Services;
using ProjectApp.ViewModels;

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
    
    public IActionResult Create()
    {
        return View();
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
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateAuctionVM model)
    {
        if (ModelState.IsValid)
        {
            var auction = new Auction
            {
                Name = model.Name,
                Description = model.Description,
                EndDate = model.EndDate,
                StartingPrice = model.StartingPrice,
                OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            _auctionService.CreateAuction(auction);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateDescription(int id, string description)
    {
        try 
        {
            _auctionService.UpdateAuctionDescription(id, description); // Anropa synkron metod
            return RedirectToAction("Details", new { id });
        } 
        catch (ArgumentException ex) 
        {
            ModelState.AddModelError("Description", ex.Message); // Lägg till felmeddelande i ModelState
            return RedirectToAction("Details", new { id }); // Returnera tillbaka till detaljer om det är ogiltigt
        }
    }
    
    
    /*
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var auction = _auctionService.Auctions.Find(id);
        if (auction == null || auction.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return NotFound();
        }

        var model = new EditAuctionVM
        {
            Id = auction.Id,
            Description = auction.Description
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditAuctionVM model)
    {
        if (ModelState.IsValid)
        {
            var auction = _auctionService.Auctions.Find(model.Id);
            if (auction == null || auction.OwnerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }

            auction.Description = model.Description; // Update only the description
            _auctionService.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }*/

}