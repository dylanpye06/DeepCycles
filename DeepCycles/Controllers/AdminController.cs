using Microsoft.AspNetCore.Mvc;
using DeepCycles.Models.Domain;
using DeepCycles.Models.ViewModels;
using DeepCycles.Repository;

namespace DeepCycles.Controllers
{
    public class AdminController(IAdminRepository adminRepository) : Controller
    {
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ActionName("LogIn")]
        public IActionResult LogIn(AdminLogInRequest adminRequest)
        {
            var adminCheckPassword = "DeepCycles";
            var adminCheckEmail = "Deepcycles@gmail.com";

            if (adminRequest.AdminEmail != adminCheckEmail)
            {
                return View("Error");
            }

            if (adminRequest.AdminPassword != adminCheckPassword)
            {
                return View("Error");
            }

            else
            {
                return RedirectToAction("AllBookings");
            }
        }

        [HttpPost]
        [ActionName("AllBookings")]
        public async Task<IActionResult> AllBookings()
        {
            var allBookings = await adminRepository.GetAllBookings();
            return View(allBookings);
        }

        [HttpGet]
        public async Task<IActionResult> EditBookings(Guid Id)
        {
            var foundBookingRequest = await adminRepository.GetBookingASync(Id);

            if (foundBookingRequest != null)
            {
                var editBookingRequest = new EditBookingRequest
                {
                    BookingId = foundBookingRequest.BookingId,
                    FullName = foundBookingRequest.FullName,
                    Email = foundBookingRequest.Email,
                    PhoneNumber = foundBookingRequest.PhoneNumber,
                    PostCode = foundBookingRequest.PostCode,
                    BookingTitle = foundBookingRequest.BookingTitle,
                    BookingDescription = foundBookingRequest.BookingDescription,
                };
                return View(editBookingRequest);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task <IActionResult> EditBookings(EditBookingRequest editBookingRequest)
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
            await adminRepository.EditBooking(booking);
            var allBookings = await adminRepository.GetAllBookings();
            return View("AllBookings", allBookings);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEditBooking(EditBookingRequest editBookingRequest)
        {
            await adminRepository.DeleteEditBooking(editBookingRequest.BookingId);
            var allBookings = await adminRepository.GetAllBookings();
            return View("AllBookings", allBookings);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBooking(Booking booking)
        {
            await adminRepository.DeleteBooking(booking.BookingId);
            var allBookings = await adminRepository.GetAllBookings();
            return View("AllBookings", allBookings);
        }
    }
}
