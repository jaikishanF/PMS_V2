using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class BookingAvailabilitiesController : Controller
    {
        private readonly IBookingAvailabilityService _service;
        private readonly IMapper _mapper;

        public BookingAvailabilitiesController(IBookingAvailabilityService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: BookingAvailabilities
        public async Task<IActionResult> Index()
        {
            var availabilities = await _service.GetAllAsync();
            var viewModel = _mapper.Map<IEnumerable<BookingAvailabilityViewModel>>(availabilities);
            return View(viewModel);
        }

        // GET: BookingAvailabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingAvailability = await _service.GetByIdAsync(id.Value);
            if (bookingAvailability == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<BookingAvailabilityViewModel>(bookingAvailability);
            return View(viewModel);
        }

        // GET: BookingAvailabilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookingAvailabilities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingAvailabilityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var bookingAvailabilityDto = _mapper.Map<BookingAvailabilityDto>(viewModel);
                await _service.AddAsync(bookingAvailabilityDto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: BookingAvailabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingAvailability = await _service.GetByIdAsync(id.Value);
            if (bookingAvailability == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<BookingAvailabilityViewModel>(bookingAvailability);
            return View(viewModel);
        }

        // POST: BookingAvailabilities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookingAvailabilityViewModel viewModel)
        {
            if (id != viewModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var bookingAvailabilityDto = _mapper.Map<BookingAvailabilityDto>(viewModel);
                await _service.UpdateAsync(bookingAvailabilityDto);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: BookingAvailabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingAvailability = await _service.GetByIdAsync(id.Value);
            if (bookingAvailability == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<BookingAvailabilityViewModel>(bookingAvailability);
            return View(viewModel);
        }

        // POST: BookingAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookingAvailabilityExists(int id)
        {
            return await _service.ExistsAsync(id);
        }
    }
}
