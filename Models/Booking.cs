﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceRoomBooking.Models
{
    [Table("booking", Schema = "public")]
    public class Booking
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("room_id")]
        public int RoomId { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        [Column("end_time")]
        public DateTime EndTime { get; set; }
        [Column("total_cost")]
        public decimal TotalCost { get; set; }
        public List<Service> Services { get; set; }
        public ConferenceRoom Room { get; set; }
    }
}
