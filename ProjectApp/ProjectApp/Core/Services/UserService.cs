using ProjectApp.Models;

namespace ProjectApp.Services;

// Core/Services/UserService.cs
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly UserManager<AppIdentityUser> _userManager;

    public UserService(UserManager<AppIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IdentityResult RegisterUser(AppIdentityUser user, string password)
    {
        // Använd CreateAsync för att registrera användaren
        var result = _userManager.CreateAsync(user, password);
    
        // Logga felmeddelanden om användarskapandet misslyckas
        if (!result.Result.Succeeded)
        {
            foreach (var error in result.Result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}"); // Logga varje fel
            }
        }
    
        return result.Result; // Observera att .Result blockerar tråden
    }

}
