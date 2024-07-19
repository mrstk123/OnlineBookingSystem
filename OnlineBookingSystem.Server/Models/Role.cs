namespace OnlineBookingSystem.Server.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
