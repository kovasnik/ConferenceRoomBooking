using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.ViewModel
{
    public class CreateBookingDto
    {
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> ServiceIds { get; set; }
    }
}
