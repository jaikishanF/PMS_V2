using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto> GetBookingByIdAsync(int id);
        Task AddBookingAsync(BookingDto bookingDto, BookingAvailabilityDto availabilityDto);
        Task UpdateBookingAsync(BookingDto bookingDto);
        Task DeleteBookingAsync(int id);
        Task<IEnumerable<BookingDto>> GetPatientBookingsAsync(string lastName);
        Task<IEnumerable<BookingAvailabilityDto>> GetAvailableSlotsAsync();
    }
}
