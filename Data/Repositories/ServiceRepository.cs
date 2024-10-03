using ConferenceRoomBooking.DTO.Interfaces;
using ConferenceRoomBooking.Models;

namespace ConferenceRoomBooking.DTO.Repositories
{
    public class ServiceRepository : IServiceRepository

    {
        private readonly BookingDbContext _context;
        public ServiceRepository(BookingDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Service service)
        {
            await _context.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<Service> GetByIdAsync(int serviceId)
        {
            return await _context.Services.FindAsync(serviceId);
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }
    }
}
