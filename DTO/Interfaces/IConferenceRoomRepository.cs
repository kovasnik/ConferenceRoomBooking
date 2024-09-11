using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IConferenceRoomRepository
    {
        Task AddAsync(ConferenceRoom conferenceRoom);
        Task DeleteAsync(ConferenceRoom conferenceRoom);
        Task UpdateAsync(ConferenceRoom conferenceRoom);
        Task<ConferenceRoom> GetRoomByIdAsync(int id);
        Task<IEnumerable<ConferenceRoom>> GetAvailableRoomAsync(DateTime startTime, DateTime endTime, int capasity);

    }
}
