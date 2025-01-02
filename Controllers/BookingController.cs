using ConferenceRoomBooking.BLL.Interfaces;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        // Connecting services using dependency injection
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingDto dtoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter valid data.");
            }

            try
            {
                var totalCost = await _bookingService.CreateBookingAsync(dtoModel);
                return Ok(totalCost);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
