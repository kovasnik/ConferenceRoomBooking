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
        public DbSet<RoomServese> RoomServeses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomServese>()
                .HasKey(pc => new { pc.ServiceId, pc.RoomId });

            modelBuilder.Entity<RoomServese>()
                .HasOne(p => p.Service)
                .WithMany(p => p.RoomServeses)
                .HasForeignKey(p => p.ServiceId);

            modelBuilder.Entity<RoomServese>()
                .HasOne(p => p.Room)
                .WithMany(p => p.RoomServeses)
                .HasForeignKey(p => p.RoomId);
        }

    }
}
