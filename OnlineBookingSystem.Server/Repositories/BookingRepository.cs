using Microsoft.EntityFrameworkCore;
using OnlineBookingSystem.Server.Models;
using OnlineBookingSystem.Server.Repositories.Interfaces;
using System.Threading.Tasks;

namespace OnlineBookingSystem.Server.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                        .Include(b => b.Car)
                        .FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _context.Bookings
                                 .Where(b => b.UserId == userId)
                                 .Include(b => b.Car)
                                 .ToListAsync();
        }
    }
}
