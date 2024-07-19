using OnlineBookingSystem.Server.Models;
using OnlineBookingSystem.Server.Repositories.Interfaces;

namespace OnlineBookingSystem.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Car> _carRepository;
        //private IRepository<Booking> _bookingRepository;
        private IBookingRepository _bookingRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Car> CarRepository => _carRepository ??= new Repository<Car>(_context);
        // public IRepository<Booking> BookingRepository => _bookingRepository ??= new Repository<Booking>(_context);
        public IBookingRepository BookingRepository => _bookingRepository ??= new BookingRepository(_context);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }

}
