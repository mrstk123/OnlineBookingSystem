using OnlineBookingSystem.Server.Models;

namespace OnlineBookingSystem.Server.Repositories.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking?> GetBookingByIdAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByUserIdAsync(int userId);
    }
}
