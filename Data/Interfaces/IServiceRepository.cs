using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IServiceRepository
    {
        Task AddAsync(Service service); // add service 
        Task UpdateAsync(Service service); // update service 
        Task DeleteAsync(Service service); // delete service 
        Task<Service> GetByIdAsync(int serviceId); // get service by id
    }
}
