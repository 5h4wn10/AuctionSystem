using System.ComponentModel.DataAnnotations;
using ProjectApp.Models;


namespace ProjectApp.ViewModels;

public class CreateAuctionVM
{
    
        [Required(ErrorMessage = "Namnet på auktionen är obligatoriskt.")]
        [StringLength(100, ErrorMessage = "Namnet får inte vara längre än 100 tecken.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatorisk.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Utgångspriset är obligatoriskt.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Utgångspriset måste vara större än 0.")]
        public decimal StartingPrice { get; set; }

        [Required(ErrorMessage = "Slutdatum är obligatoriskt.")]
        [DataType(DataType.DateTime)] 
        public DateTime EndDate { get; set; }
        
        
        public Auction ToAuction(string ownerId)
        {
                return new Auction
                {
                        Name = Name,
                        Description = Description,
                        StartingPrice = StartingPrice,
                        EndDate = EndDate,
                        OwnerId = ownerId
                };
        }
    
}