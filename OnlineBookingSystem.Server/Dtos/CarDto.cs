using OnlineBookingSystem.Server.Models;

namespace OnlineBookingSystem.Server.Dtos
{
    public class CarDto
    {
        public int CarId { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public string CarBrand { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public string? ImageUrl { get; set; }

        public string VehicleNumber { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
        public string Transmission { get; set; } = string.Empty;
        public AvailabilityStatus Availability { get; set; }

        public IFormFile? Image { get; set; }
    }
}
