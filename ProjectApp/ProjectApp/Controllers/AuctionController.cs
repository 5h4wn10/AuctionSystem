using System.Security.Claims;
using ProjectApp.Models;
using ProjectApp.Services;
using ProjectApp.ViewModels;

namespace ProjectApp.Controllers;

using Microsoft.AspNetCore.Mvc;

public class AuctionController : Controller
{
    private readonly IAuctionRepository _auctionService;
    private readonly ILogger<AuctionController> _logger;

    public AuctionController(IAuctionRepository auctionService)
    {
        _auctionService = auctionService;
    }

    /*public IActionResult Index()
    {
        var auctions = _auctionService.GetActiveAuctions(); // Anropa synkron metod
        return View(auctions);
    }*/
    
    public IActionResult Index()
    {
        // Hämta endast auktioner som tillhör den inloggade användaren
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var auctions = _auctionService.GetAuctionsByUser(userId); // Ny metod som hämtar auktioner baserat på userId
        return View(auctions);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        var auction = _auctionService.GetAuctionDetails(id); // Hämta auktionens detaljer
        if (auction == null)
        {
            return NotFound(); // Returnera 404 om auktionen inte finns
        }
    // Här är din ActiveAuctions Action Method
    public IActionResult ActiveAuctions()
    {
        var activeAuctions = _auctionService.GetActiveAuctions(); // Hämta alla aktiva auktioner
        var viewModel = activeAuctions.Select(a => new ActiveAuctionVM
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            StartingPrice = a.StartingPrice,
            EndDate = a.EndDate,
            OwnerId = a.OwnerId,
            OwnerName = _userManager.FindByIdAsync(a.OwnerId).Result.UserName // Hämta användarnamnet för OwnerId
        }).ToList(); // Skapa en lista av ActiveAuctionVM

        return View(viewModel);  // Returnera vy med auktionerna
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

    
    
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var auction = _auctionService.GetAuctionDetails(id);
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
            try
            {
                // Flyttad logik till servicen
                _auctionService.EditAuctionDescription(model.Id, model.Description);
                return RedirectToAction("Details", new { id = model.Id });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Description", ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(); // Returnera 403 Forbidden om användaren inte har rättigheter
            }
        }

        return View(model);
    }
    
    
        }
        return View(model);
    }*/

}