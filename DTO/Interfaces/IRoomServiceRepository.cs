using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IRoomServiceRepository
    {
        Task AddAsync(RoomService roomService); // add roomservice
        Task DeleteAsync(RoomService roomService); // delete roomservice
        Task UpdateAsync(RoomService roomService); // update roomservice
    }
}
