namespace ConferenceRoomBooking.Models
{
    public class RoomServese
    {
        public int ServiceId { get; set; }
        public int RoomId { get; set; }
        public ConferenceRoom Room { get; set; }
        public Service Service { get; set; }
        public List<RoomServese> RoomServeses { get; set; }
    }
}
