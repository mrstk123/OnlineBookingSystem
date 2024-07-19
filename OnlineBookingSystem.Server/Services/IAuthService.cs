using OnlineBookingSystem.Server.Models;

namespace OnlineBookingSystem.Server.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<Tuple<string?, User>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
