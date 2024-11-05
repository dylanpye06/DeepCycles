using DeepCycles.Data;
using DeepCycles.Models.Domain;
using DeepCycles.Models.ViewModels;
using DeepCycles.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeepCycles.Controllers
{
    public class CustomerController(ICustomerRepository customerRepository) : Controller
    {
        [HttpGet]
        public IActionResult Map()
        {
            return View("Map");
        }

        [HttpGet]
        public IActionResult MakeABooking()
        {
            return View("MakeABooking");
        }

        [HttpPost]
        public async Task<IActionResult> MakeABooking(MakeABookingRequest makeABookingRequest)
        {
            var booking = new Booking
            {
                BookingId = makeABookingRequest.BookingId,
                FullName = makeABookingRequest.FullName,
                Email = makeABookingRequest.Email,
                PhoneNumber = makeABookingRequest.PhoneNumber,
                PostCode = makeABookingRequest.PostCode,// check post code distance has to be reasonable difference 
                BookingTitle = makeABookingRequest.BookingTitle,
                BookingDescription = makeABookingRequest.BookingDescription,
                CollectionTime = makeABookingRequest.CollectionTime,
            };
            // TODO can we tidy up this code?
            if (booking.FullName != null && booking.Email != null && booking.PhoneNumber != null && booking.PostCode != null && booking.BookingTitle != null
                && booking.BookingDescription != null && booking.BookingTitle != null && booking.BookingDescription != null && booking.CollectionTime != null)

            // use tool given to check all boxes are valid https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0

            {
                booking.BookingId = Guid.NewGuid();

                //caching - take look at Dictionary<string,string> for instance

                var booking1 = await customerRepository.DistanceMatrixResult(booking);
 //               await customerRepository.SaveBooking(booking1);

                // Id only gets given to the booking once it has been uploaded and saved to the database
                // may need to write a method to give it an id before hand as only want to save to the databse on the filan submit button
                return RedirectToAction("ViewBooking", booking1);
            }
            else return View("MakeABooking", makeABookingRequest);
        }


        [HttpGet]
        [ActionName("ViewBooking")]
        public IActionResult ViewBooking(Booking booking)
        {
 //           var foundBooking = await customerRepository.GetBookingASync(BookingId);
            return View(booking);
        }

        [HttpGet]
        public IActionResult EditBooking([Bind] Booking booking)
        {
            if (booking != null)
            {
                var editBookingRequest = new EditBookingRequest
                {
                    BookingId = booking.BookingId,
                    FullName = booking.FullName,
                    Email = booking.Email,
                    PhoneNumber = booking.PhoneNumber,
                    PostCode = booking.PostCode,
                    BookingTitle = booking.BookingTitle,
                    BookingDescription = booking.BookingDescription,
                    CollectionAndDropOffCharge = booking.CollectionAndDropOffCharge,
                    CollectionTime = booking.CollectionTime,
                };
                return View(editBookingRequest);
            }
            else
            {
                return View("Error");
            }
        }


        [HttpPost]
        [ActionName("EditBooking")]
        public async Task<IActionResult> EditBooking(EditBookingRequest editBookingRequest)
        {
            var booking = new Booking
            {
                BookingId = editBookingRequest.BookingId,
                FullName = editBookingRequest.FullName,
                Email = editBookingRequest.Email,
                PhoneNumber = editBookingRequest.PhoneNumber,
                PostCode = editBookingRequest.PostCode,
                BookingTitle = editBookingRequest.BookingTitle,
                BookingDescription = editBookingRequest.BookingDescription,
                CollectionAndDropOffCharge = editBookingRequest.CollectionAndDropOffCharge,
                CollectionTime = editBookingRequest.CollectionTime
            };
            await customerRepository.EditBooking(booking);
            return RedirectToAction("ViewBooking", booking);
        }

        [HttpPost]
        [ActionName("SaveBooking")]
        public async Task<IActionResult> SaveBooking(Booking booking)
        {
            await customerRepository.SaveBooking(booking);
            return View("HomePage");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEditBooking(EditBookingRequest editAndConfirmBookingRequest)
        {
            await customerRepository.DeleteEditBooking(editAndConfirmBookingRequest.BookingId);
            return View("HomePage");

            // show success message
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(Booking booking)
        {
            await customerRepository.DeleteBooking(booking.BookingId);
            return View("HomePage");

            // show success message
        }

        [HttpPost]
        [ActionName("AllBikes")]
        public async Task<IActionResult> AllBikes()
        {
            var allBikes = await customerRepository.GetAllBikes();
            return View(allBikes);
        }

        [HttpGet]
        public async Task<IActionResult> ViewBike(Guid BikeId)
        {
            var bike = await customerRepository.GetBike(BikeId);
            return View(bike);
        }

        [HttpPost]
        [ActionName("ViewBike")]
        public async Task<IActionResult> ViewBike()
        {
            var allBikes = await customerRepository.GetAllBikes();
            return View("AllBikes", allBikes);
        }
    }
}
