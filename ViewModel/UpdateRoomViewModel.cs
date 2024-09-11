namespace ConferenceRoomBooking.ViewModel
{
    public class UpdateRoomViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public decimal? CostPerHour { get; set; }
    }
}
