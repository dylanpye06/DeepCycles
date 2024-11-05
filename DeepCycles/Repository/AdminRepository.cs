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
        public async Task<Booking?> DeleteEditBooking(Guid Id)
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
        public async Task<Booking?> GetBookingASync(Guid Id)
        {
            return await databaseLink.Bookings.Where(x => x.BookingId == Id).FirstOrDefaultAsync();
        }

        public async Task<Booking?> EditBooking(Booking booking)
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
        public async Task<Booking?> DeleteBooking(Guid id)
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

        public async Task<IEnumerable<HandmadeBikes>> GetAllBikes()
        {
            return await databaseLink.HandmadeBikes.ToListAsync();
        }

        public async Task<HandmadeBikes> AddBike(HandmadeBikes handmadeBikes)
        {
            await databaseLink.HandmadeBikes.AddAsync(handmadeBikes);
            await databaseLink.SaveChangesAsync();
            return handmadeBikes;
        }

        public async Task<HandmadeBikes?> EditBike(HandmadeBikes handmadeBikes)
        {
            var checkBike = await databaseLink.HandmadeBikes.FindAsync(handmadeBikes.BikeId);

            if (checkBike != null)
            {
                checkBike.BikeName = handmadeBikes.BikeName;
                checkBike.BikeDescription = handmadeBikes.BikeDescription;
                checkBike.Price = handmadeBikes.Price;
                checkBike.DisplayImage = handmadeBikes.DisplayImage;
                checkBike.DisplayImagePath = handmadeBikes.DisplayImagePath;

                await databaseLink.SaveChangesAsync();
                return checkBike;
            }
            return null;
        }

        public async Task<HandmadeBikes?> DeleteBike(Guid Id)
        {
            var existingBike = await databaseLink.HandmadeBikes.FindAsync(Id);

            if (existingBike != null)
            {
                databaseLink.HandmadeBikes.Remove(existingBike);
                await databaseLink.SaveChangesAsync();
                return existingBike;
            }
            return null;
        }

        public async Task<HandmadeBikes?> GetBike(Guid Id)
        {
            return await databaseLink.HandmadeBikes.Where(x => x.BikeId == Id).FirstOrDefaultAsync();
        }
    }
}
