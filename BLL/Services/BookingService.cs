using AutoMapper;
using ConferenceRoomBooking.BLL.Interfaces;
using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.DTO.Repositories;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.BLL.Services
{
    public class BookingService : IBookingService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public BookingService(
            IServiceRepository serviceRepository,
            IBookingRepository bookingRepository,
            IConferenceRoomRepository roomRepository,
            IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<decimal> CreateBookingAsync(CreateBookingDto dtoModel)
        {
            // Check the reservation for a conference room at a specific time
            bool isBooked = await _bookingRepository.IsAvilableAsync(dtoModel.RoomId, dtoModel.StartTime, dtoModel.EndTime);
            if (isBooked)
            {
                throw new InvalidOperationException("This room is not available at the that time.");
            }

            var room = await _roomRepository.GetRoomByIdAsync(dtoModel.RoomId);
            if (room == null)
            {
                throw new KeyNotFoundException("Conference room not found.");
            }

            var booking = _mapper.Map<Booking>(dtoModel);

            // TotalCost calculation
            TimeSpan duration = dtoModel.EndTime - dtoModel.StartTime;
            for (int hour = 0; hour < duration.TotalHours; hour++)
            {
                DateTime currentHour = dtoModel.StartTime.AddHours(hour);

                if (currentHour.Hour >= 6 && currentHour.Hour < 9)
                {
                    booking.TotalCost += room.CostPerHour * 0.9m;
                }
                else if (currentHour.Hour >= 12 && currentHour.Hour < 14)
                {
                    booking.TotalCost += room.CostPerHour * 1.15m;
                }
                else if (currentHour.Hour >= 18 && currentHour.Hour < 23)
                {
                    booking.TotalCost += room.CostPerHour * 0.8m;
                }
                else
                {
                    booking.TotalCost += room.CostPerHour;
                }
            }

            // Adding cost of services
            if (dtoModel.ServiceIds != null)
            {
                foreach (var serviceId in dtoModel.ServiceIds)
                {
                    var service = await _serviceRepository.GetByIdAsync(serviceId);
                    if (service == null)
                    {
                        throw new KeyNotFoundException($"Service with ID {serviceId} not found.");
                    }
                    booking.TotalCost += service.Cost;
                }
            }

            await _bookingRepository.CreateAsync(booking);
            return booking.TotalCost;
        }
    }
}
