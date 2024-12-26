using ConferenceRoomBooking.BLL.Interfaces;
using ConferenceRoomBooking.BLL.Services;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        // Connecting repositories using dependency injection
        private readonly IServiceService _serviceService;
        
        public ServiceController(IServiceService serviceService) 
        {
            _serviceService = serviceService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateServiceDto dtoModel)
        {
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var serviceId = await _serviceService.AddAsync(dtoModel);
                return Ok(serviceId);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with adding the room. Please try another time.");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(int serviceId)
        {
            try
            {
                var isDeleted = await _serviceService.DeleteAsync(serviceId);
                if (isDeleted)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpgrateAsync([FromBody] UpdateServiceDto dtoModel)
        {
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter data");
            }
            try
            {
                var isUpdated = await _serviceService.UpgrateAsync(dtoModel);
                if (isUpdated)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
