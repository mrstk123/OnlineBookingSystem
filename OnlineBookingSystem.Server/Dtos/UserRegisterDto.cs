using System.ComponentModel.DataAnnotations;

namespace OnlineBookingSystem.Server.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        // [StringLength(12, MinimumLength = 6, ErrorMessage = "You must specify password between 6 and 12 characters")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
    }

}
