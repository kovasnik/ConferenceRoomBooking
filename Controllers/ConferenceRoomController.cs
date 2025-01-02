using ConferenceRoomBooking.BLL.Interfaces;
using ConferenceRoomBooking.BLL.Services;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConferenceRoomController : Controller
    {
        // Connecting repositories using dependency injection
        private readonly IConferenceRoomService _conferenceRoomService;

        public ConferenceRoomController(IConferenceRoomService conferenceRoomService) 
        { 
            _conferenceRoomService = conferenceRoomService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddConfereceRoom([FromBody] CreateRoomDto roomWithServices)
        {
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var roomId = await _conferenceRoomService.AddConfereceRoom(roomWithServices);
                return Ok(roomId);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with adding the room. Please try another time.");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteConferenceRoom(int roomId)
        {
            // Search for a conference room by id
            var isDeleted = await _conferenceRoomService.DeleteConferenceRoom(roomId);
            if (!isDeleted) 
            {
                return BadRequest("Id does not exist");
            }
            return NoContent(); // 204
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateConfirenceRoom([FromBody] UpdateRoomDto dtoModel)
        {
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _conferenceRoomService.UpdateConfirenceRoom(dtoModel);
            if (!isUpdated)
            {
                return BadRequest("Conference room not found");
            }
            return Ok();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableConfirenceRoom(DateTime startTime, DateTime endTime, int capasity)
        {
            try
            {
                var viewModels = await _conferenceRoomService.GetAvailableConfirenceRoom(startTime, endTime, capasity);
                return Ok(viewModels);
            }
            catch (InvalidOperationException ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
