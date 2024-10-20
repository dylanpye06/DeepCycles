using DeepCycles.Data;
using DeepCycles.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DeepCycles.Repository
{
    public class AdminRepository(DataBaseLink databaseLink) : IAdminRepository 
    {
        public async Task<IEnumerable<Booking>> GetAllBookings()
        {
            return await databaseLink.Bookings.ToListAsync();
        }
        public async Task<Booking> DeleteEditBooking(Guid Id)
        {
            var existingBooking = await databaseLink.Bookings.FindAsync(Id);
            if (existingBooking != null)
            {
                databaseLink.Bookings.Remove(existingBooking);
                await databaseLink.SaveChangesAsync();
                return existingBooking;
            }
            return null;
        }
        public async Task<Booking> GetBookingASync(Guid Id)
        {
            return await databaseLink.Bookings.Where(x => x.BookingId == Id).FirstOrDefaultAsync();
        }

        public async Task<Booking> EditBooking(Booking booking)
        {
            var checkBooking = await databaseLink.Bookings.FindAsync(booking.BookingId);

            if (checkBooking != null)
            {
                checkBooking.FullName = booking.FullName;
                checkBooking.Email = booking.Email;
                checkBooking.PhoneNumber = booking.PhoneNumber;
                checkBooking.PostCode = booking.PostCode;
                checkBooking.BookingTitle = booking.BookingTitle;
                checkBooking.BookingTitle = booking.BookingTitle;

                await databaseLink.SaveChangesAsync();
                return checkBooking;
            }
            return null;
        }
        public async Task<Booking> DeleteBooking(Guid id)
        {
            var existingBooking = await databaseLink.Bookings.FindAsync(id);

            if (existingBooking != null)
            {
                databaseLink.Bookings.Remove(existingBooking);
                await databaseLink.SaveChangesAsync();
                return existingBooking;
            }
            return null;
        }

    }
}
