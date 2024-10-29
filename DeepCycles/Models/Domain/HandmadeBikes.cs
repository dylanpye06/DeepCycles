using System.ComponentModel.DataAnnotations;

namespace DeepCycles.Models.Domain
{
    public class HandmadeBikes
    {
        [Key]
        public Guid BikeId { get; set; }
        public required string BikeName { get; set; }
        public required string BikeDescription { get; set; }
        public required string Price { get; set; }
    }
}
