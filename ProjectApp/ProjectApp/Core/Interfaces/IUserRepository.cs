using ProjectApp.Models;

namespace ProjectApp.Services;

// Core/Interfaces/IUserService.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public interface IUserRepository
{
    IdentityResult RegisterUser(AppIdentityUser user, string password);
}

