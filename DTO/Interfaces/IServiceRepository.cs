using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IServiceRepository
    {
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Service service);
        Task<Service> GetByIdAsync(int serviceId);
    }
}
