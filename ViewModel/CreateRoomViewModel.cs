using ConferenceRoomBooking.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.ViewModel
{
    public class CreateRoomViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal CostPerHour { get; set; }
        public List<int> ServiceIds { get; set; }
    }
}
