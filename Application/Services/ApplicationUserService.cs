using Application.Interfaces;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ApplicationUserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationUserDto> GetPatientByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<ApplicationUserDto>(user);
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetPatientByLastNameAsync(string lastName)
        {
            var users = await _userRepository.GetByLastNameAsync(lastName);
            return _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
        }

        public async Task<IEnumerable<ApplicationUserDto>> GetAllPatientsAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
        }

        public async Task UpdatePatientAsync(ApplicationUserDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeletePatientAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
