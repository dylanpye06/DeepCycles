using DeepCycles.Models.Domain;
using DeepCycles.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DeepCycles.Repository
{
    public interface ICustomerRepository
    {
        Task<Booking> MakeABooking(Booking booking1);
        Task<Booking?> EditBooking(Booking booking);
        Task<Booking> GetBookingASync(Guid bookingId);
        Task<Booking?> DeleteEditBooking(Guid id);
        Task<Booking?> DeleteBooking(Guid id);
        Task<Booking> DistanceMatrixResult(Booking booking);
        Task<IEnumerable<HandmadeBikes>> GetAllBikes();
        Task<HandmadeBikes?> GetBike(Guid Id);
        Task<Booking> SaveBooking(Booking booking);
    }
}
