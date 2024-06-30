using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users
                .Include(u => u.InsuranceDetails)
                .Include(u => u.EmergencyDetails)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetByLastNameAsync(string lastName)
        {
            return await _context.Users
                .Where(u => u.LastName == lastName)
                .Include(u => u.InsuranceDetails)
                .Include(u => u.EmergencyDetails)
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.InsuranceDetails)
                .Include(u => u.EmergencyDetails)
                .ToListAsync();
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
