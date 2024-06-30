using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> GetByLastNameAsync(string lastName);
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(string id);
    }
}
