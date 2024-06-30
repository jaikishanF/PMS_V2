using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookingAvailabilityRepository
    {
        Task<IEnumerable<BookingAvailability>> GetAllAsync();
        Task<BookingAvailability> GetByIdAsync(int id);
        Task AddAsync(BookingAvailability bookingAvailability);
        Task UpdateAsync(BookingAvailability bookingAvailability);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
