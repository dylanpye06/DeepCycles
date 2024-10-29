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

            //https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-core-mvc-using-cookie/

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
                return RedirectToAction("AdminPage");
            }
        }

        [HttpGet]
        [ActionName("AdminPage")]
        public IActionResult AdminPage()
        {
            return View("AdminPage");
        }

        [HttpPost]
        [ActionName("AllBookings")]
        public async Task<IActionResult> AllBookings()
        {
            var allBookings = await adminRepository.GetAllBookings();
            return View(allBookings);

            /// doesnt need to edit all or see all the information but needs a place to cpture any notes or anthing important ect pick up time
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
                    CollectionTime = foundBookingRequest.CollectionTime,
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
                CollectionTime = editBookingRequest.CollectionTime,
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

        [HttpPost]
        [ActionName("AllBikes")]
        public async Task<IActionResult> AllBikes()
        {
            var allBikes = await adminRepository.GetAllBikes();
            return View(allBikes);
        }

        [HttpGet]
        [ActionName("EditBikes")]
        public async Task<IActionResult> EditBikes(Guid Id)
        {
            var foundBike = await adminRepository.GetBike(Id);

            if (foundBike != null)
            {
                var editHandmadeBikeRequest = new EditBikeRequest
                {
                    BikeId =foundBike.BikeId,
                    BikeName = foundBike.BikeName,
                    BikeDescription = foundBike.BikeDescription,
                    Price = foundBike.Price,
                };
                return View(editHandmadeBikeRequest);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBikes(EditBikeRequest editHandmadeBikeRequest)
        {
            var handmadeBikes = new HandmadeBikes
            {
                BikeId = editHandmadeBikeRequest.BikeId,
                BikeName = editHandmadeBikeRequest.BikeName,
                BikeDescription = editHandmadeBikeRequest.BikeDescription,
                Price = editHandmadeBikeRequest.Price,
            };
            await adminRepository.EditBike(handmadeBikes);
            var allBikes = await adminRepository.GetAllBikes();
            return View("AllBikes", allBikes);
        }

        [HttpGet]
        [ActionName("AddABike")]
        public IActionResult AddABike()
        { return View("AddABike"); }

        [HttpPost]
        public async Task<IActionResult> AddABike(AddBikeRequest addHandMadeBikeRequest)
        {
            var handmadeBike = new HandmadeBikes
            {
                BikeName = addHandMadeBikeRequest.BikeName,
                BikeDescription = addHandMadeBikeRequest.BikeDescription,
                Price = addHandMadeBikeRequest.Price,
            };
            if (handmadeBike.BikeName != null && handmadeBike.BikeDescription != null && handmadeBike.Price != null)
            {
                await adminRepository.AddBike(handmadeBike);
                var allBikes = await adminRepository.GetAllBikes();
                return View("AllBikes", allBikes);
            }
            else return View("AddABike", addHandMadeBikeRequest);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBike(HandmadeBikes handmadeBikes)
        {
            await adminRepository.DeleteBike(handmadeBikes.BikeId);
            var allBikes = await adminRepository.GetAllBikes();
            return View("AllBikes", allBikes);
        }
    }
}
