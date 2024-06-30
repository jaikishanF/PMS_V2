using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookingAvailabilityService
    {
        Task<IEnumerable<BookingAvailabilityDto>> GetAllAsync();
        Task<BookingAvailabilityDto> GetByIdAsync(int id);
        Task AddAsync(BookingAvailabilityDto bookingAvailabilityDto);
        Task UpdateAsync(BookingAvailabilityDto bookingAvailabilityDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
