using System.Text.Json.Serialization;

namespace OnlineBookingSystem.Server.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string CarName { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public string CarType { get; set; } = string.Empty;
        public string CarBrand { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; }

        public string VehicleNumber { get; set; } = string.Empty;
        public int NumberOfSeats { get; set; }
        public string Transmission { get; set; } = string.Empty;

        public AvailabilityStatus Availability { get; set; }

        // Navigation property
        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }
    }
}
