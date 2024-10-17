using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Models; // Inkludera din vy-modell
using ProjectApp.Services; // Inkludera tjänster
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly SignInManager<AppIdentityUser> _signInManager;
    private readonly IUserService _userService;

    public AccountController(SignInManager<AppIdentityUser> signInManager, IUserService userService)
    {
        _signInManager = signInManager;
        _userService = userService;
    }

    // Inloggningsmetod
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            var result = _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false).Result; // Använd Result för synkront
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Auction"); // Redirecta till startsidan eller annan sida
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(model);
    }

    // Registreringsmetod
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppIdentityUser 
            { 
                UserName = model.Username, 
                Email = model.Email 
            };

            var result = _userService.RegisterUser(user, model.Password);
        
            Console.WriteLine($"Register result: {result.Succeeded}"); // Loggar resultatet

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }



    public IActionResult Logout()
    {
        _signInManager.SignOutAsync(); // Logout
        return RedirectToAction("Index", "Home");
    }
}
