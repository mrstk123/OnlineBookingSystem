﻿namespace OnlineBookingSystem.Server.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; } 
        public int CarId { get; set; } 
        public string BookingNo { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public decimal RentalRate { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public decimal TotalCost { get; set; }

        // Navigation property
        public User User { get; set; }
        public Car Car { get; set; } 
    }
}
