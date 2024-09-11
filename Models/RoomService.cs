using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.Models
{
    [Table("room_serice", Schema = "public")]
    public class RoomService
    {
        // transitional table
        [Column("service_id")]
        public int ServiceId { get; set; }
        [Column("room_id")]
        public int RoomId { get; set; }
        public ConferenceRoom Room { get; set; }
        public Service Service { get; set; }
    }
}
