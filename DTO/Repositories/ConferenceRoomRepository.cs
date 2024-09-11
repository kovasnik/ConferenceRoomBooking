using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.Models;
using ConferenceRoomBooking.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DTO.Repositories
{
    public class ConferenceRoomRepository : IConferenceRoomRepository
    {
        private readonly BookingDbContext _context;
        private readonly IBookingRepository _bookingRepository;
        public ConferenceRoomRepository(BookingDbContext context, IBookingRepository bookingRepository) 
        {
            _context = context;
            _bookingRepository = bookingRepository;
        }
        public async Task AddAsync(ConferenceRoom conferenceRoom)
        {
            await _context.AddAsync(conferenceRoom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ConferenceRoom room)
        {
            _context.ConferenceRooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ConferenceRoom>> GetAvailableRoomAsync(DateTime startTime, DateTime endTime, int capasity)
        {
            var rooms = await _context.ConferenceRooms.Where(r => r.Capacity >= capasity).Include(r => r.RoomServices).ToListAsync();

            var availableRooms = new List<ConferenceRoom>();

            foreach(var room in rooms)
            {
                bool isBooked = await _bookingRepository.IsAvilableAsync(room.Id, startTime, endTime);
                if (!isBooked)
                {
                    availableRooms.Add(room);
                }
            }

            return availableRooms;
            //return await _context.ConferenceRooms
            //    .Where(r => r.Capacity >= capasity && !_context.Bookings
            //    .Any(b => b.RoomId == r.Id && b.StartTime < endTime && b.EndTime > startTime))
            //    .ToListAsync();
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
