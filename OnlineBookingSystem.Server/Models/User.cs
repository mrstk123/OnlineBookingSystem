namespace OnlineBookingSystem.Server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        // Navigation property
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
