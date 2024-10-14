using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProjectApp.Data;
using ProjectApp.Models;

namespace ProjectApp.Controllers;
using Microsoft.AspNetCore.Mvc;

// Implementera metoder för att hantera budgivning


 [Authorize] // Kräver inloggning för att få tillgång till alla metoder
    public class BidController : Controller
    {
        private readonly AuctionDBContext _context;

        public BidController(AuctionDBContext context)
        {
            _context = context;
        }

        // GET: Bids/Create
        public IActionResult Create(int auctionId)
        {
            // Passa auctionId till vyn så att budet kan kopplas till auktionen
            ViewBag.AuctionId = auctionId;
            return View();
        }

        // POST: Bids/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bid bid, int auctionId)
        {
            if (ModelState.IsValid)
            {
                bid.AuctionId = auctionId; // Sätta AuctionId för budet
                bid.BidTime = DateTime.Now; // Sätta tidpunkt för budet
                _context.Bids.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Auction", new { id = auctionId }); // Återgå till auktionens detaljer
            }
            ViewBag.AuctionId = auctionId;
            return View(bid);
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var bid = await _context.Bids
                .Include(b => b.Auction) // Hämta auktionen för budet
                .FirstOrDefaultAsync(m => m.Id == id);

            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // GET: Bids/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bids/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bid bid)
        {
            if (id != bid.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bid);
        }

        // GET: Bids/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var bid = await _context.Bids
                .Include(b => b.Auction) // Hämta auktionen för budet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid != null)
            {
                _context.Bids.Remove(bid);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Återgå till lista över bud
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.Id == id);
        }
    }