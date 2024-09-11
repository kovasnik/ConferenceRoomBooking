using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Interfaces
{
    public interface IBookingRepository
    {
        Task CreateAsync(Booking booking);
        Task<Booking> GetByIdAsync(int bookingId);
        Task<bool> IsAvilableAsync(int roomId, DateTime startTime, DateTime endTime);
    }
}
