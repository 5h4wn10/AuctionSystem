﻿using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Models;

public class RegisterVM
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    // Ny egenskap för användarnamn
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }
}