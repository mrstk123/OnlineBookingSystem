using OnlineBookingSystem.Server.Models;

namespace OnlineBookingSystem.Server.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Car> CarRepository { get; }
        // IRepository<Booking> BookingRepository { get; }
        IBookingRepository BookingRepository { get; }
        Task SaveAsync();
    }
}
