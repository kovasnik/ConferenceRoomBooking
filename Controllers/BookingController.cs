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

        public BookingController(IBookingRepository bookingRepository, IConferenceRoomRepository roomRepository, IServiceRepository serviceRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _serviceRepository = serviceRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter data");
            }
            // Check the reservation for a conference room at a specific time
            bool isBooked = await _bookingRepository.IsAvilableAsync(viewModel.RoomId, viewModel.StartTime, viewModel.EndTime);
            if (isBooked)
            {
                return BadRequest("This room is not available at the that time.");
            }

            var room = await _roomRepository.GetRoomByIdAsync(viewModel.RoomId);
            if (room == null)
            {
                return NotFound("Conference room not found.");
            }

            var booking = new Booking
            {
                RoomId = viewModel.RoomId,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                TotalCost = 0
            };

            // TotalCost calculation
            TimeSpan duration = viewModel.EndTime - viewModel.StartTime;
            for (int hour = 0;  hour < duration.TotalHours; hour++)
            {
                DateTime currentHour = viewModel.StartTime.AddHours(hour);

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
            if (viewModel.ServiceIds != null)
            {
                var service = new Service();
                foreach (var serviceId in viewModel.ServiceIds)
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
