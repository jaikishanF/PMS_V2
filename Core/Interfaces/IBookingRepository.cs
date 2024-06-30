using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task AddBookingAsync(Booking booking, BookingAvailability availability);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(int id);
        Task<IEnumerable<Booking>> GetPatientBookingsAsync(string lastName);
        Task<IEnumerable<BookingAvailability>> GetAvailableSlotsAsync();
    }
}
