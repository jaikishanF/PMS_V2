using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BookingAvailabilityRepository : IBookingAvailabilityRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingAvailabilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingAvailability>> GetAllAsync()
        {
            return await _context.BookingAvailability.ToListAsync();
        }

        public async Task<BookingAvailability> GetByIdAsync(int id)
        {
            return await _context.BookingAvailability.FindAsync(id);
        }

        public async Task AddAsync(BookingAvailability bookingAvailability)
        {
            await _context.BookingAvailability.AddAsync(bookingAvailability);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookingAvailability bookingAvailability)
        {
            _context.Entry(bookingAvailability).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bookingAvailability = await _context.BookingAvailability.FindAsync(id);
            if (bookingAvailability != null)
            {
                _context.BookingAvailability.Remove(bookingAvailability);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.BookingAvailability.AnyAsync(e => e.ID == id);
        }
    }
}
