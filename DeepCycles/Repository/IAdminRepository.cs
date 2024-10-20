using DeepCycles.Models.Domain;

namespace DeepCycles.Repository
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Booking>> GetAllBookings();
        Task<Booking> DeleteEditBooking(Guid Id);
        Task<Booking> GetBookingASync(Guid Id);
        Task<Booking> EditBooking(Booking booking);
        Task<Booking> DeleteBooking(Guid id);
    }
}
