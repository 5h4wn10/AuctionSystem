using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProjectApp.Models;
using ProjectApp.Services;
using ProjectApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApp.Controllers;

public class AuctionController : Controller
{
    private readonly IAuctionRepository _auctionService;
    private readonly ILogger<AuctionController> _logger;
    private readonly UserManager<AppIdentityUser> _userManager;

    public AuctionController(IAuctionRepository auctionService, ILogger<AuctionController> logger, UserManager<AppIdentityUser> userManager)
    {
        _auctionService = auctionService;
        _logger = logger;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        // Hämta endast auktioner som tillhör den inloggade användaren
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var auctions = _auctionService.GetAuctionsByUser(userId); // Ny metod som hämtar auktioner baserat på userId
        return View(auctions);
    }

    public IActionResult Create()
    {
        return View(new CreateAuctionVM());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateAuctionVM model)
    {
        if (ModelState.IsValid)
        {
            var auction = model.ToAuction(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _auctionService.CreateAuction(auction);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Details(int id)
    {
        var auction = _auctionService.GetAuctionDetails(id);
        if (auction == null) return NotFound();

        var ownerName = _userManager.FindByIdAsync(auction.OwnerId).Result.UserName;
        var viewModel = DetailsVM.FromAuction(auction, ownerName, _userManager); // Lägg till _userManager
        return View(viewModel);
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
    public IActionResult Bid(int id)
    {
        var auction = _auctionService.GetAuctionDetails(id);
        if (auction == null) return NotFound();

        var ownerName = _userManager.FindByIdAsync(auction.OwnerId).Result.UserName;
        var viewModel = BidVM.FromAuction(auction, ownerName);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult PlaceBid(int auctionId, decimal bidAmount)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            _auctionService.PlaceBid(new Bid
            {
                AuctionId = auctionId,
                Amount = bidAmount,
                UserId = userId,
                BidTime = DateTime.Now
            });
            return RedirectToAction("Details", new { id = auctionId });
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return RedirectToAction("Details", new { id = auctionId });
        }
    }
    
    
    public IActionResult OngoingAuctionsWithBids()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var auctions = _auctionService.GetOngoingAuctionsWithUserBids(userId);

        var viewModels = auctions.Select(a =>
        {
            var ownerName = _userManager.FindByIdAsync(a.OwnerId).Result.UserName;
            return AuctionVM.FromAuction(a, ownerName);
        }).ToList();

        return View(viewModels); // Skapa en vy som listar dessa auktioner
    }
    
    
    public IActionResult CompletedAuctionsWon()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var auctions = _auctionService.GetCompletedAuctionsWonByUser(userId);

        var viewModels = auctions.Select(a =>
        {
            var ownerName = _userManager.FindByIdAsync(a.OwnerId).Result.UserName;
            return AuctionVM.FromAuction(a, ownerName);
        }).ToList();

        return View(viewModels); // Skapa en vy som listar dessa auktioner
    }

}
