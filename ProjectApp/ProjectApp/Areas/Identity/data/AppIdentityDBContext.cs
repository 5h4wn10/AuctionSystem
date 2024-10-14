using ProjectApp.Models;

namespace ProjectApp.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppIdentityDBContext : IdentityDbContext<AppIdentityUser>
{
    public AppIdentityDBContext(DbContextOptions<AppIdentityDBContext> options)
        : base(options)
    {
    }

    // Lägg till DbSet för andra entiteter här
}
