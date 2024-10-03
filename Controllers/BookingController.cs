using AutoMapper;
using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.DTO.Repositories;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        // Connecting repositories using dependency injection
        private readonly IServiceRepository _serviceRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IConferenceRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IConferenceRoomRepository roomRepository, IServiceRepository serviceRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingDto dtoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter data");
            }
            // Check the reservation for a conference room at a specific time
            bool isBooked = await _bookingRepository.IsAvilableAsync(dtoModel.RoomId, dtoModel.StartTime, dtoModel.EndTime);
            if (isBooked)
            {
                return BadRequest("This room is not available at the that time.");
            }

            var room = await _roomRepository.GetRoomByIdAsync(dtoModel.RoomId);
            if (room == null)
            {
                return NotFound("Conference room not found.");
            }

            var booking = _mapper.Map<Booking>(dtoModel);
            //var booking = new Booking
            //{
            //    RoomId = dtoModel.RoomId,
            //    StartTime = dtoModel.StartTime,
            //    EndTime = dtoModel.EndTime,
            //    TotalCost = 0
            //};

            // TotalCost calculation
            TimeSpan duration = dtoModel.EndTime - dtoModel.StartTime;
            for (int hour = 0;  hour < duration.TotalHours; hour++)
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
                var service = new Service();
                foreach (var serviceId in dtoModel.ServiceIds)
                {
                    service = await _serviceRepository.GetByIdAsync(serviceId);
                    booking.TotalCost += service.Cost;
                }
            }

            await _bookingRepository.CreateAsync(booking);

            return Ok(booking.TotalCost);
        }
    }
}
