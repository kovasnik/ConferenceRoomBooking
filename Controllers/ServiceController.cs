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
    public class ServiceController : Controller
    {
        // Connecting repositories using dependency injection
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        
        public ServiceController(IServiceRepository serviceRepository, IMapper mapper) 
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateServiceDto dtoModel)
        {
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Pass the checked values ​​to the model
            var service = _mapper.Map<Service>(dtoModel);
            //var service = new Service
            //{
            //    Name = dtoModel.Name,
            //    Description = dtoModel.Description,
            //    Cost = dtoModel.Cost
            //};

            await _serviceRepository.AddAsync(service);
            return Ok(service.Id);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(int serviceId)
        {
            // Search for a service by id
            var service = await _serviceRepository.GetByIdAsync(serviceId);

            if (service is null)
            {
                return BadRequest("Servicee does not exist");
            }

            await _serviceRepository.DeleteAsync(service);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpgrateAsync([FromBody] UpdateServiceDto dtoModel)
        {
            // Model checks
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter data");
            }


            // Search for a service by id
            var service = await _serviceRepository.GetByIdAsync(dtoModel.Id);

            // Pass the checked values ​​to the model
            _mapper.Map(dtoModel, service);
            //service.Name = dtoModel.Name;
            //service.Description = dtoModel.Description;
            //service.Cost = dtoModel.Cost;

            await _serviceRepository.UpdateAsync(service); 
            return Ok();
        }
    }
}
