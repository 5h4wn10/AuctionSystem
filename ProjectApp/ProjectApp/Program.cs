using Microsoft.AspNetCore.Identity;
using ProjectApp.Data;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using ProjectApp.Models;
using ProjectApp.Services; // Importera dina modeller

var builder = WebApplication.CreateBuilder(args);

// Konfigurera DbContext för auktioner
builder.Services.AddDbContext<AuctionDBContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AuctionDbConnection"))); // Använder MySQL för AuctionDB

// Konfigurera DbContext för Identity
builder.Services.AddDbContext<AppIdentityDBContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection"))); // Använder MySQL för IdentityDB

// Lägg till Identity-tjänster och konfiguration
builder.Services.AddDefaultIdentity<AppIdentityUser>(options => 
        options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AppIdentityDBContext>(); // Använd IdentityDB för Identity

// Lägg till MVC och kontroller
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuctionRepository, AuctionRepository>();

var app = builder.Build();

// Konfigurera HTTP-förfrågningspipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Använd autentisering för Identity
app.UseAuthorization();  // Använd auktorisering

// Definiera standardrutt för MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Om du har Razor Pages

app.Run(); // Starta applikationen