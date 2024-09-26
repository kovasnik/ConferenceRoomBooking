using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomBooking.ViewModel
{
    public class UpdateServiceDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a name for service")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Cost needs to be higher than 0")]
        public decimal Cost { get; set; }
    }
}
