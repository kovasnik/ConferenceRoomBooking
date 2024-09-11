using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DTO.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;
        public BookingRepository(BookingDbContext context) 
        {
            _context = context;
        }
        public async Task CreateAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsAvilableAsync(int roomId, DateTime startTime, DateTime endTime)
        {
            return await _context.Bookings.AnyAsync(b => b.RoomId == roomId && b.StartTime < endTime && b.EndTime > startTime);
        }

        public async Task<Booking> GetByIdAsync(int bookingId)
        {
            return await _context.Bookings.FindAsync(bookingId);
        }
    }
}
