using Application.Interfaces;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookingAvailabilityService : IBookingAvailabilityService
    {
        private readonly IBookingAvailabilityRepository _repository;
        private readonly IMapper _mapper;

        public BookingAvailabilityService(IBookingAvailabilityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingAvailabilityDto>> GetAllAsync()
        {
            var bookingAvailabilities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingAvailabilityDto>>(bookingAvailabilities);
        }

        public async Task<BookingAvailabilityDto> GetByIdAsync(int id)
        {
            var bookingAvailability = await _repository.GetByIdAsync(id);
            return _mapper.Map<BookingAvailabilityDto>(bookingAvailability);
        }

        public async Task AddAsync(BookingAvailabilityDto bookingAvailabilityDto)
        {
            var bookingAvailability = _mapper.Map<BookingAvailability>(bookingAvailabilityDto);
            await _repository.AddAsync(bookingAvailability);
        }

        public async Task UpdateAsync(BookingAvailabilityDto bookingAvailabilityDto)
        {
            var bookingAvailability = _mapper.Map<BookingAvailability>(bookingAvailabilityDto);
            await _repository.UpdateAsync(bookingAvailability);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
