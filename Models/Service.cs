using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.Models
{
    [Table("service", Schema = "public")]

    public class Service
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("cost")]
        public decimal Cost { get; set; }
        public List<RoomService> RoomServices { get; set; }

    }
}
