namespace DeepCycles.Models.ViewModels
{
    public class AddBikeRequest
    {
        public required string BikeName { get; set; }
        public required string BikeDescription { get; set; }
        public required string Price { get; set; }
    }
}
