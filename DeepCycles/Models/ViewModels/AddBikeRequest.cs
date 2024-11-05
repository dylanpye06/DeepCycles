using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DeepCycles.Models.ViewModels
{
    public class AddBikeRequest
    {
        public Guid BikeId { get; set; }
        public required string BikeName { get; set; }
        public required string BikeDescription { get; set; }
        public required string Price { get; set; }
        public string? DisplayImagePath { get; set; }

        // if it doesnt work remove the NotMapped
        [NotMapped]
        public IFormFile? DisplayImage { get; set; }
    }
}

