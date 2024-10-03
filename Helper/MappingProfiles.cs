using AutoMapper;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;

namespace ConferenceRoomBooking.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<CreateServiceDto, Service>();
            CreateMap<CreateRoomDto, ConferenceRoom>();
            CreateMap<ConferenceRoom, AvailableRoomsDto>()
                .ForMember(dest => dest.ServiceIds, 
                opt => opt.MapFrom(src => src.RoomServices.Select(rs => rs.ServiceId).ToList()));
            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.TotalCost, 
                opt => opt.MapFrom(src => 0));
        }
    }
}
