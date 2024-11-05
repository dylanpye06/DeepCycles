namespace DeepCycles.Models.Domain
{
    public class Booking
    {
        public Guid BookingId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string PostCode { get; set; }
        public required string BookingTitle {  get; set; }
        public required string BookingDescription { get; set;}
        public string? CollectionAndDropOffCharge { get; set; }
        public required string CollectionTime { get; set; }
    }
}
