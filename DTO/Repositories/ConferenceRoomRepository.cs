using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DTO.Repositories
{
    public class ConferenceRoomRepository : IConferenceRoomRepository
    {
        private readonly BookingDbContext _context;
        public ConferenceRoomRepository(BookingDbContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(ConferenceRoom conferenceRoom)
        {
            _context.AddAsync(conferenceRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.ConferenceRooms.FindAsync(id);
            if(room != null)
            {
                _context.ConferenceRooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ConferenceRoom>> GetAvailableRoomAsync(DateTime startTime, DateTime endTime, int capasity)
        {
            return await _context.ConferenceRooms
                .Where(r => r.Capacity >= capasity && !_context.Bookings
                .Any(b => b.RoomId == r.Id && b.StartTime < endTime && b.EndTime > startTime))
                .ToListAsync();
        }

        public async Task<ConferenceRoom> GetRoomByIdAsync(int id)
        {
            return await _context.ConferenceRooms.FindAsync(id);
        }

        public async Task UpdateAsync(ConferenceRoom conferenceRoom)
        {
            _context.ConferenceRooms.Update(conferenceRoom);
            await _context.SaveChangesAsync();
        }
    }
}
