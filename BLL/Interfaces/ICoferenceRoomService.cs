using ConferenceRoomBooking.ViewModel;

namespace ConferenceRoomBooking.BLL.Interfaces
{
    public interface IConferenceRoomService
    {
        Task<int> AddConfereceRoom(CreateRoomDto roomWithServices);
        Task<bool> DeleteConferenceRoom(int roomId);
        Task<bool> UpdateConfirenceRoom(UpdateRoomDto dtoModel);
        Task<IEnumerable<AvailableRoomsDto>> GetAvailableConfirenceRoom(DateTime startTime, DateTime endTime, int capasity);

    }
}
