using ConferenceRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DTO
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomService>()
                .HasKey(rs => new { rs.ServiceId, rs.RoomId });

            modelBuilder.Entity<RoomService>()
                .HasOne(rs => rs.Service)
                .WithMany(s => s.RoomServices)
                .HasForeignKey(rs => rs.ServiceId);

            modelBuilder.Entity<RoomService>()
                .HasOne(rs => rs.Room)
                .WithMany(cr => cr.RoomServices)
                .HasForeignKey(rs => rs.RoomId);
        }

    }
}
