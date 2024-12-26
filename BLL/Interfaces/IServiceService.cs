using ConferenceRoomBooking.ViewModel;

namespace ConferenceRoomBooking.BLL.Interfaces
{
    public interface IServiceService
    {
        Task<int> AddAsync(CreateServiceDto dtoModel);
        Task<bool> DeleteAsync(int serviceId);
        Task<bool> UpgrateAsync(UpdateServiceDto dtoModel);
    }
}
