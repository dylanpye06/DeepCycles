using DeepCycles.Data;
using Microsoft.EntityFrameworkCore;
using DeepCycles.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using DeepCycles.Models.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;


namespace DeepCycles.Repository
{
    public class CustomerRepository(DataBaseLink databaseLink) : ICustomerRepository
    {
        private readonly string _apiKey = "AIzaSyBT-Kt3tSG4EFehxs3WHudUBD-fZNW6ONU";

        public async Task<Booking> MakeABooking(Booking booking1)
        {
            await databaseLink.Bookings.AddAsync(booking1);
            await databaseLink.SaveChangesAsync();
            return booking1;
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
                checkBooking.BookingDescription = booking.BookingDescription;

                await databaseLink.SaveChangesAsync();
                return checkBooking;
            }
            return null;
        }

        public async Task<Booking> GetBookingASync(Guid bookingId)
        {
            return await databaseLink.Bookings.Where(x => x.BookingId == bookingId).FirstOrDefaultAsync();
        }

        public async Task<Booking> DeleteEditBooking(Guid id)
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

        public async Task<Booking> DistanceMatrixResult(Booking booking)
     //         public async Task<int> DistanceMatrixResult(string shopPostcode, string customerPostcode )
        {
            var origin = "LL573BJ";
            var destination = booking.PostCode;

            DistanceMatrixResult result = new();
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&key={_apiKey}";

            using (HttpClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(jsonResponse);

                    if (data["rows"] != null && data["rows"].HasValues)
                    {
                        var element = data["rows"][0]["elements"][0];
                        result.Duration = element["duration"]["text"].ToString();
                    }
                }
                // map to a concrte type deserilaize https://www.newtonsoft.com/json/help/html/deserializeobject.htm
            }
            string firstTwoChars = result.Duration[..2];
            int duration = Int32.Parse(firstTwoChars);
            var charge = "£" + duration * 1.2;

            // add £ in th view and also gabriels needs a way to chnage the multiplyer and consider thrshold / deals, create bands - put everything in config

            var booking1 = new Booking
            {
                BookingId = booking.BookingId,
                FullName = booking.FullName,
                Email = booking.Email,
                PhoneNumber = booking.PhoneNumber,
                PostCode = booking.PostCode,
                BookingTitle = booking.BookingTitle,
                BookingDescription = booking.BookingDescription,
                CollectionAndDropOffCharge = charge,
                CollectionTime = booking.CollectionTime,
            };
                return booking1;
        }
    }
}