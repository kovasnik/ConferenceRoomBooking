using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.ViewModel
{
    public class CreateServiceDto
    {
        [Required(ErrorMessage = "Please enter a name for service")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Cost needs to be higher than 0")]
        public decimal Cost { get; set; }
    }
}
