using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return await _context.Booking
                .Include(b => b.Patient)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Booking
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(b => b.ID == id);
        }

        public async Task AddBookingAsync(Booking booking, BookingAvailability availability)
        {
            await _context.Booking.AddAsync(booking);

            // Attach the availability entity and mark it as modified
            var trackedAvailability = await _context.BookingAvailability.FindAsync(availability.ID);
            if (trackedAvailability != null)
            {
                _context.Entry(trackedAvailability).CurrentValues.SetValues(availability);
            }
            else
            {
                _context.BookingAvailability.Attach(availability);
                _context.Entry(availability).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Booking.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Booking>> GetPatientBookingsAsync(string lastName)
        {
            return await _context.Booking
                .Where(b => b.Patient.LastName == lastName)
                .Include(b => b.Patient)
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingAvailability>> GetAvailableSlotsAsync()
        {
            return await _context.BookingAvailability
                .Where(a => a.IsAvailable)
                .ToListAsync();
        }
    }
}
