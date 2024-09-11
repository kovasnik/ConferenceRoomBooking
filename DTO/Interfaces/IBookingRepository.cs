using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateAsync(Booking booking); // create bookind
        Task<Booking> GetByIdAsync(int bookingId); // get booking by id
        Task<bool> IsAvilableAsync(int roomId, DateTime startTime, DateTime endTime); // get all available booking on specific time
    }
}
