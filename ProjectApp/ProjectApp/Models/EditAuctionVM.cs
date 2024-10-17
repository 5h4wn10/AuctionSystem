using Microsoft.Build.Framework;

namespace ProjectApp.Models;

public class EditAuctionVM
{
    public int Id { get; set; }

    [Required]
    public string Description { get; set; }
}