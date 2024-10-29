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
            if (booking.FullName != null && booking.Email != null
            && booking.PostCode != null && booking.BookingTitle != null && booking.BookingDescription != null)

            // use tool given to check all boxes are valid https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0

            {
                //caching - take look at Dictionary<string,string> for instance

                var booking1 = await customerRepository.DistanceMatrixResult(booking);
                await customerRepository.MakeABooking(booking1);
                return RedirectToAction("ViewBooking", booking1);
            }
            else return View("MakeABooking", makeABookingRequest);
        }


        [HttpGet]
        [ActionName("ViewBooking")]
        public async Task<IActionResult> ViewBooking(Guid BookingId)
        {
            var foundBooking = await customerRepository.GetBookingASync(BookingId);
            return View(foundBooking);
        }

        [HttpGet]
        public async Task<IActionResult> EditBooking(Guid Id)
        {
            var booking = await customerRepository.GetBookingASync(Id);

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
                CollectionTime = editBookingRequest.CollectionTime
            };
            await customerRepository.EditBooking(booking);
            return RedirectToAction("ViewBooking", booking);
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

        [HttpGet]
        public IActionResult HandmadeBikes()
        {
            return View("AllBikes");

           // we need a new table i the databse where gabriel can upload his owen bikes to the datasbaase and then this view must display all names of the bike E.G- Enduro 29er medium
           // and then ech one has a link that opens another view with then shows all information and pictures
        }

        [HttpPost]
        public IActionResult HandmadeBikes(HandmadeBikes handmadeBikes)
        {
            return View("AllBikes");

            // we need to put the whole bike databsase table into a list which cn be accessed from the view / method

            // each link with represent a single Id in the list of bikes

            // in the next View we will write something like 

            // foreach(bike in list)
                
                // dispay this...........
        }
    }
}
