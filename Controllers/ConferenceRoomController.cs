using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.DTO.Repositories;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenceRoomController : Controller
    {
        private readonly IConferenceRoomRepository _conferenceRoomRepository;
        private readonly IRoomServiceRepository _roomServiceRepository;
        public ConferenceRoomController(IConferenceRoomRepository roomRepository, IRoomServiceRepository roomServiceRepository) 
        { 
            _conferenceRoomRepository = roomRepository;
            _roomServiceRepository = roomServiceRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddConfereceRoom([FromBody] CreateRoomViewModel roomWithServices)
        {

            var serviceIds = roomWithServices.ServiceIds;
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter an existing conference room");
            }
            else if (roomWithServices.Capacity <= 4)
            {
                return BadRequest("Capacity needs to be higher than 4 people");
            }
            else if (roomWithServices.CostPerHour <= 0)
            {
                return BadRequest("Cost per hour needs to be higher than 0");
            }

            var room = new ConferenceRoom
            {
                Name = roomWithServices.Name,
                Description = roomWithServices.Description,
                Capacity = roomWithServices.Capacity,
                CostPerHour = roomWithServices.CostPerHour
            };


            await _conferenceRoomRepository.AddAsync(room);

            if (room.RoomServices == null)
            {
                room.RoomServices = new List<RoomService>();
            }

            foreach (var serviceId in serviceIds)
            {
                await _roomServiceRepository.AddAsync(new RoomService { ServiceId = serviceId, RoomId = room.Id });
                //room.RoomServices.Add(new RoomService { ServiceId = serviceId, RoomId = room.Id });
            }

            return Ok(room.Id);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteConferenceRoom(int roomId)
        {
            var room = await _conferenceRoomRepository.GetRoomByIdAsync(roomId);
            if (room != null)
            {
                await _conferenceRoomRepository.DeleteAsync(room);
                return NoContent(); 
            }

                return BadRequest("Id does not exist");
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateConfirenceRoom([FromBody] UpdateRoomViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill in all fields");
            }
            else if (viewModel.Capacity <= 4)
            {
                return BadRequest("Capacity needs to be higher than 4 people");
            }
            else if (viewModel.CostPerHour <= 0)
            {
                return BadRequest("Cost per hour needs to be higher than 0");
            }

            var existingRoom = await _conferenceRoomRepository.GetRoomByIdAsync(viewModel.Id);

            if (existingRoom == null)
            {
                return NotFound("Conference room not found");
            }

            existingRoom.Name = viewModel.Name ?? existingRoom.Description;
            existingRoom.CostPerHour = viewModel.CostPerHour ?? existingRoom.CostPerHour;
            existingRoom.Capacity = viewModel.Capacity ?? existingRoom.Capacity;
            existingRoom.Description = viewModel.Description ?? existingRoom.Description;

            await _conferenceRoomRepository.UpdateAsync(existingRoom);
            return Ok();
        }

        [HttpPost("available")]
        public async Task<IActionResult> GetAvailableConfirenceRoom(DateTime startTime, DateTime endTime, int capasity)
        {
            if (startTime.Date != endTime.Date)
            {
                return BadRequest("Booking must be made on the same day");
            }
            TimeSpan startLimit = new TimeSpan(6, 0, 0);  // 06:00
            TimeSpan endLimit = new TimeSpan(24, 0, 0);   // 24:00

            if (startTime.TimeOfDay < startLimit || endTime.TimeOfDay > endLimit)
            {
                return BadRequest("Booking time must be between 6:00 AM and 12:00 PM");
            }

            IEnumerable<ConferenceRoom> availableRooms = await _conferenceRoomRepository.GetAvailableRoomAsync(startTime, endTime, capasity);
            var viewModels = availableRooms.Select(r => new AvailableRoomsViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Capacity = r.Capacity,
                ServiceIds = r.RoomServices.Select(rs => rs.ServiceId).ToList()
            });

            if (viewModels is null)
            {
                return BadRequest("No available rooms with that time and capasity");
            }

            return Ok(viewModels);
        }
    }
}
