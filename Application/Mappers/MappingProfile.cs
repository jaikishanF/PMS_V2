using Core.DTOs;
using AutoMapper;
using Core.Entities;
using Application.ViewModels;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<BookingAvailability, BookingAvailabilityDto>().ReverseMap();
            CreateMap<BookingAvailabilityViewModel, BookingAvailabilityDto>().ReverseMap();
            CreateMap<BookingViewModel, BookingDto>().ReverseMap();

            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<EmergencyDetails, EmergencyDetailsDto>().ReverseMap();
            CreateMap<InsuranceDetails, InsuranceDetailsDto>().ReverseMap();

            CreateMap<ApplicationUserViewModel, ApplicationUserDto>().ReverseMap();
            CreateMap<EmergencyDetailsViewModel, EmergencyDetailsDto>().ReverseMap();
            CreateMap<InsuranceDetailsViewModel, InsuranceDetailsDto>().ReverseMap();
        }
    }
}
