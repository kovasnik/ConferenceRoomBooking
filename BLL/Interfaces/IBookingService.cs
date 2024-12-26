using ConferenceRoomBooking.ViewModel;

namespace ConferenceRoomBooking.BLL.Interfaces
{
    public interface IBookingService
    {
        Task<decimal> CreateBookingAsync(CreateBookingDto dtoModel);
    }
}
