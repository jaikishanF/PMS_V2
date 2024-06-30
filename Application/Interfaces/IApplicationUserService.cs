using Core.DTOs;

namespace Application.Interfaces
{
    public interface IApplicationUserService
    {
        Task<ApplicationUserDto> GetPatientByIdAsync(string id);
        Task<IEnumerable<ApplicationUserDto>> GetPatientByLastNameAsync(string lastName);
        Task<IEnumerable<ApplicationUserDto>> GetAllPatientsAsync();
        Task UpdatePatientAsync(ApplicationUserDto userDto);
        Task DeletePatientAsync(string id);
    }
}
