using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.Models
{
    [Table("conference_room", Schema = "public")]
    public class ConferenceRoom
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("capacity")]
        public int Capacity { get; set; }
        [Column("cost_per_hour")]
        public decimal CostPerHour { get; set; }
        // table ConferenceRoom has connection many to many with table service via RoomService table
        public List<RoomService>? RoomServices { get; set; }
        // table ConferenceRoom has connection one to many with table Booking 
        public List<Booking>? Bookings { get; set; }
    }
}
