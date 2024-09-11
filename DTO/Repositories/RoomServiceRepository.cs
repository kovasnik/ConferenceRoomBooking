using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Repositories
{
    public class RoomServiceRepository : IRoomServiceRepository
    {
        private readonly BookingDbContext _context;
        public RoomServiceRepository(BookingDbContext context) 
        {
            _context = context;   
        }
        public async Task AddAsync(RoomService roomService)
        {
            await _context.RoomServices.AddAsync(roomService);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RoomService roomService)
        {
            _context.RoomServices.Remove(roomService);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomService roomService)
        {
            _context.Update(roomService);
            await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<RoomService>> GetByRoomIdAsync(int roomId)
        //{
        //    var roomServices = await _context.RoomServices..FindAsync().;
        //    return roomServices;
        //}

    }
}
