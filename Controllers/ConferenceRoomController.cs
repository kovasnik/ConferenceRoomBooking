using ConferenceRoomBooking.DTO.Repositories;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenceRoomController : Controller
    {
        private readonly ConferenceRoomRepository _conferenceRoomRepository;
        public ConferenceRoomController(ConferenceRoomRepository roomRepository) 
        { 
            _conferenceRoomRepository = roomRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddConfereceRoom([FromBody] RoomAndService roomAndService)
        {

            var serviceIds = roomAndService.ServiceIds;
            if (roomAndService.Id == null)
            {
                return BadRequest("Please enter an existing conference room");
            }
            else if (roomAndService.Capacity <= 4)
            {
                return BadRequest("Capacity needs to be higher than 4 people");
            }

            var room = new ConferenceRoom();
            room.Id = roomAndService.Id;
            room.Name = roomAndService.Name;
            room.Description = roomAndService.Description;
            room.Capacity = roomAndService.Capacity;
            room.CostPerHour = roomAndService.CostPerHour;

            foreach (var serviceId in serviceIds)
            {
                room.RoomServices.Add(new RoomService { ServiceId = serviceId, RoomId = room.Id });
            }

            await _conferenceRoomRepository.AddAsync(room);

            return Ok(room.Id);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteConferenceRoom(int roomId)
        {
            await _conferenceRoomRepository.DeleteAsync(roomId);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateConfirenceRoom([FromBody] ConferenceRoom room)
        {
            if (room == null)
            {
                return BadRequest("Please enter the new information correctly");
            }
            else if (room.Capacity <= 4)
            {
                return BadRequest("Capacity needs to be higher than 4 people");
            }
            await _conferenceRoomRepository.UpdateAsync(room);
            return Ok();
        }

        [HttpPost("available")]
        public async Task<IActionResult> GetAvailableConfirenceRoom(DateTime startTime, DateTime endTime, int capasity)
        {
            IEnumerable<ConferenceRoom> availableRooms = new List<ConferenceRoom>();
            availableRooms = await _conferenceRoomRepository.GetAvailableRoomAsync(startTime, endTime, capasity);

            return Ok(availableRooms);
        }
    }
}
