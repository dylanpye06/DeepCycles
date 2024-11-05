using DeepCycles.Models.Domain;

namespace DeepCycles.Repository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking?> DeleteEditBooking(Guid Id);
        Task<Booking?> GetBookingASync(Guid Id);
        Task<Booking?> EditBooking(Booking booking);
        Task<Booking?> DeleteBooking(Guid id);


        Task<HandmadeBikes> AddBike(HandmadeBikes bike);
        Task<HandmadeBikes?> EditBike (HandmadeBikes bike);
        Task<IEnumerable<HandmadeBikes>> GetAllBikes();
        Task<HandmadeBikes?> GetBike(Guid Id);
        Task<HandmadeBikes?> DeleteBike(Guid Id);
    }
}
