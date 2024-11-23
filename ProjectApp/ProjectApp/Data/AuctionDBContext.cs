using ProjectApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectApp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AuctionDBContext : IdentityDbContext
{
    public AuctionDBContext(DbContextOptions<AuctionDBContext> options)
        : base(options)
    { }

    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfigurera relationer om det behövs
        modelBuilder.Entity<Auction>()
            .HasMany(a => a.Bids)
            .WithOne(b => b.Auction)
            .HasForeignKey(b => b.AuctionId)
            .OnDelete(DeleteBehavior.Cascade); // Om en auktion tas bort, tas också buden bort
    }

public DbSet<ProjectApp.Models.EditAuctionVM> EditAuctionVM { get; set; } = default!;
}