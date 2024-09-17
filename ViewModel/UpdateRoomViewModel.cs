using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.ViewModel
{
    public class UpdateRoomViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a conference room name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(5, int.MaxValue, ErrorMessage = "Capacity needs to be higher than 4 people")]
        public int Capacity { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Cost per hour needs to be higher than 0")]
        public decimal CostPerHour { get; set; }
    }
}
