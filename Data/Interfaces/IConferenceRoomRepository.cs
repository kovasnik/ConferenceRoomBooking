using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IConferenceRoomRepository
    {
        Task AddAsync(ConferenceRoom conferenceRoom); // add conference room
        Task DeleteAsync(ConferenceRoom conferenceRoom); // delete conference room
        Task UpdateAsync(ConferenceRoom conferenceRoom); // update conference room
        Task<ConferenceRoom> GetRoomByIdAsync(int id); // get conference room by id
        Task<IEnumerable<ConferenceRoom>> GetAvailableRoomAsync(DateTime startTime, DateTime endTime, int capasity); // get available conference room on specific time

    }
}
