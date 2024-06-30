using Core.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);
            return _mapper.Map<BookingDto>(booking);
        }

        public async Task AddBookingAsync(BookingDto bookingDto, BookingAvailabilityDto availabilityDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);
            var availability = _mapper.Map<BookingAvailability>(availabilityDto);

            booking.Patient = await _userManager.FindByIdAsync(bookingDto.PatientId);
            if (booking.Patient == null)
            {
                throw new Exception("Patient not found");
            }

            try
            {
                await _bookingRepository.AddBookingAsync(booking, availability);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("The booking you are trying to add has been modified or deleted.", ex);
            }
        }

        public async Task UpdateBookingAsync(BookingDto bookingDto)
        {
            var booking = _mapper.Map<Booking>(bookingDto);
            try
            {
                await _bookingRepository.UpdateBookingAsync(booking);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("The booking you are trying to update has been modified or deleted.", ex);
            }
        }

        public async Task DeleteBookingAsync(int id)
        {
            try
            {
                await _bookingRepository.DeleteBookingAsync(id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbUpdateConcurrencyException("The booking you are trying to delete has been modified or deleted.", ex);
            }
        }

        public async Task<IEnumerable<BookingDto>> GetPatientBookingsAsync(string lastName)
        {
            var bookings = await _bookingRepository.GetPatientBookingsAsync(lastName);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<IEnumerable<BookingAvailabilityDto>> GetAvailableSlotsAsync()
        {
            var availability = await _bookingRepository.GetAvailableSlotsAsync();
            return _mapper.Map<IEnumerable<BookingAvailabilityDto>>(availability);
        }
    }
}
