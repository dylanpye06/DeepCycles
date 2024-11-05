using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeepCycles.Models.Domain
{
    public class HandmadeBikes
    {
        [Key]
        public Guid BikeId { get; set; }
        public required string BikeName { get; set; }
        public required string BikeDescription { get; set; }
        public required string Price { get; set; }
        public string? DisplayImagePath { get; set; }

        [NotMapped]
        public required IFormFile DisplayImage { get; set; }
    }
}
