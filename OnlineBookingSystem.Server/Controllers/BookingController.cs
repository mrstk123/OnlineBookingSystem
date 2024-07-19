using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBookingSystem.Server.Dtos;
using OnlineBookingSystem.Server.Models;
using OnlineBookingSystem.Server.Repositories.Interfaces;

namespace OnlineBookingSystem.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "User")]
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(BookingDto booking)
        {
            var newBooking = new Booking()
            {
                UserId = booking.UserId,
                CarId = booking.CarId,
                BookingNo= booking.BookingNo,
                CustomerName = booking.CustomerName,
                BookingDate = DateTime.Now.Date,
                RentalRate = booking.RentalRate,
                RentalStartDate = booking.RentalStartDate,
                RentalEndDate = booking.RentalEndDate,
                TotalCost = booking.TotalCost
            };
            await _unitOfWork.BookingRepository.AddAsync(newBooking);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetBookingById), new { id = newBooking.BookingId }, newBooking);
        }

        [Authorize(Policy = "User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _unitOfWork.BookingRepository.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            booking.RentalStartDate = booking.RentalStartDate.ToLocalTime().Date;
            booking.RentalEndDate = booking.RentalEndDate.ToLocalTime().Date;
            return Ok(booking);
        }

        [HttpGet("GetBookingNumber")]
        public string GetBookingNumber()
        {
            // Get current timestamp in milliseconds
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            // Generate a random 4-digit number (between 1000 and 9999)
            Random random = new Random();
            int randomNumber = random.Next(1000, 10000);

            // Combine timestamp and random number to create the booking number
            string bookingNumber = $"{timestamp}{randomNumber}";

            return bookingNumber;
        }

        [Authorize(Policy = "User")]
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByUser(int userId)
        {
            var bookings = await _unitOfWork.BookingRepository.GetBookingsByUserIdAsync(userId);
            if (bookings == null)
            {
                return NotFound();
            }
            foreach (var booking in bookings)
            {
                booking.RentalStartDate = booking.RentalStartDate.ToLocalTime().Date;
                booking.RentalEndDate = booking.RentalEndDate.ToLocalTime().Date;
            }
            return Ok(bookings);
        }
    }

}
