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
        // table service has connection many to meny with table conference room via RoomService table
        public List<RoomService> RoomServices { get; set; }

    }
}
