﻿



namespace DeepCycles.Models.ViewModels
{
    public class ConfirmBookingRequest
    {
        public Guid BookingId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required int PhoneNumber { get; set; }
        public required string PostCode { get; set; }
        public required string BookingTitle { get; set; }
        public required string BookingDescription { get; set; }
        public string? CollectionAndDropOffCharge { get; set; }
    }
}
