using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IRoomServiceRepository
    {
        Task AddAsync(RoomService roomService);
        Task DeleteAsync(RoomService roomService);
        Task UpdateAsync(RoomService roomService);
    }
}
