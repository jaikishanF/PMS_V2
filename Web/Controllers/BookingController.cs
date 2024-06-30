using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(IBookingService bookingService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _userManager = userManager;
        }

        private async Task PrepareIndexViewData(BookingViewModel booking)
        {
            ViewBag.AppointmentTypes = Enum.GetValues(typeof(AppointmentType))
                                       .Cast<AppointmentType>()
                                       .Select(a => new SelectListItem
                                       {
                                           Value = ((int)a).ToString(),
                                           Text = a.ToString(),
                                           Selected = a == booking.AppointmentType
                                       });

            var patients = await _userManager.Users.ToListAsync();

            ViewBag.Patients = patients.Select(u => new SelectListItem
            {
                Value = u.Id,
                Text = u.Fullname
            }).ToList();

            if (!patients.Any())
            {
                ModelState.AddModelError("", "No patients available to book.");
            }
        }

        public async Task<IActionResult> Index(BookingViewModel booking = null)
        {
            if (booking == null)
            {
                var defaultPatient = await _userManager.Users.FirstOrDefaultAsync();
                booking = new BookingViewModel
                {
                    BookingDate = DateTime.Today,
                    StartTime = TimeSpan.FromHours(DateTime.Now.Hour),
                    EndTime = TimeSpan.FromHours(DateTime.Now.Hour + 1),
                    AppointmentType = AppointmentType.Consultation,
                    PatientId = defaultPatient?.Id,
                    PatientFullName = defaultPatient?.Fullname
                };
            }

            await PrepareIndexViewData(booking);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel viewModel)
        {
            var bookingDto = _mapper.Map<BookingDto>(viewModel);
            var availability = (await _bookingService.GetAvailableSlotsAsync())
                                   .FirstOrDefault(ba => ba.Date.Date == bookingDto.BookingDate.Date &&
                                                         ba.StartTime == bookingDto.StartTime &&
                                                         ba.EndTime == bookingDto.EndTime &&
                                                         ba.IsAvailable);

            if (availability == null)
            {
                ModelState.AddModelError("", "No available slots match your booking request.");
                await PrepareIndexViewData(viewModel);
                return View("Index", viewModel);
            }

            if (ModelState.IsValid)
            {
                availability.IsAvailable = false;
                try
                {
                    await _bookingService.AddBookingAsync(bookingDto, availability);
                    TempData["SuccessMessage"] = "Booking successfully created.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. The booking you attempted to create may have been modified or deleted by another user.");
                }
            }

            await PrepareIndexViewData(viewModel);
            return View("Index", viewModel);
        }


        public async Task<IActionResult> GetAvailableSlots()
        {
            var availability = await _bookingService.GetAvailableSlotsAsync();
            return Json(availability);
        }

        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            var viewModel = _mapper.Map<IEnumerable<BookingViewModel>>(bookings);

            if (!viewModel.Any())
            {
                ViewBag.error = "No bookings available.";
            }

            return View(viewModel);
        }

        public async Task<IActionResult> GetPatientBookings(string lastName)
        {
            var bookings = await _bookingService.GetPatientBookingsAsync(lastName);
            var viewModel = _mapper.Map<IEnumerable<BookingViewModel>>(bookings);

            if (!viewModel.Any())
            {
                ViewBag.Error = "No bookings found for the specified last name.";
            }

            return View(viewModel);
        }
    }
}
