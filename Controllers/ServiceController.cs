using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.DTO.Repositories;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceController(IServiceRepository serviceRepository) 
        {
            _serviceRepository = serviceRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateServiceViewModel viewModel)
        {
            if (viewModel.Name == null)
            {
                return BadRequest("Please enter a name for service ");
            }
            else if (viewModel.Cost <= 0)
            {
                return BadRequest("Cost needs to be higher than 0");
            }

            var service = new Service
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Cost = viewModel.Cost
            };

            await _serviceRepository.AddAsync(service);
            return Ok(service.Id);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int serviceId)
        {
            var service = await _serviceRepository.GetByIdAsync(serviceId);

            if (service is null)
            {
                return BadRequest("Servicee does not exist");
            }

            await _serviceRepository.DeleteAsync(service);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpgrateAsync([FromBody] UpdateServiceViewModel viewModel)
        {
            if (viewModel.Name == null)
            {
                return BadRequest("Please enter a name for service ");
            }
            else if (viewModel.Cost <= 0)
            {
                return BadRequest("Cost needs to be higher than 0");
            }

            var service = await _serviceRepository.GetByIdAsync(viewModel.Id);

            service.Name = viewModel.Name ?? service.Name;
            service.Description = viewModel.Description ?? service.Description;
            service.Cost = viewModel.Cost ?? service.Cost;

            await _serviceRepository.UpdateAsync(service); 
            return Ok();
        }
    }
}
