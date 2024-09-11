using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.ViewModel
{
    public class CreateServiceViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
    }
}
