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
        public IActionResult DistanceCalculator()
        {
            return View("DistanceCalculator");
        }

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
                PostCode = makeABookingRequest.PostCode,
                BookingTitle = makeABookingRequest.BookingTitle,
                BookingDescription = makeABookingRequest.BookingDescription,
            };
            // TODO can we tidy up this code?
            if (booking.FullName != null && booking.Email != null
            && booking.PostCode != null && booking.BookingTitle != null && booking.BookingDescription != null)
            {
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
    }
}
