using AutoMapper;
using ConferenceRoomBooking.BLL.Interfaces;
using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.BLL.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(CreateServiceDto dtoModel)
        {

            // Pass the checked values ​​to the model
            var service = _mapper.Map<Service>(dtoModel);

            await _serviceRepository.AddAsync(service);
            return service.Id;
        }

        public async Task<bool> DeleteAsync(int serviceId)
        {
            // Search for a service by id
            var service = await _serviceRepository.GetByIdAsync(serviceId);

            if (service is null)
            {
                return false;
            }

            await _serviceRepository.DeleteAsync(service);
            return true;
        }

        public async Task<bool> UpgrateAsync(UpdateServiceDto dtoModel)
        {
            // Search for a service by id
            var service = await _serviceRepository.GetByIdAsync(dtoModel.Id);
            if (service is null)
            {
                return false;
            }
            // Pass the checked values ​​to the model
            _mapper.Map(dtoModel, service);

            await _serviceRepository.UpdateAsync(service);
            return true;
        }
    }
}
