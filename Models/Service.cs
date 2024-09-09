namespace ConferenceRoomBooking.Models
{
    public class Service
    {
        int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public List<RoomServese> RoomServeses { get; set; }

    }
}
