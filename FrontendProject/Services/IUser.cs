using FrontendProject.Models;

namespace FrontendProject.Services
{
    public interface IUser
    {
        Task Registration(User user);
        Task<UserRead> Authenticate(string username, string password);
        Task<IEnumerable<UserRead>> GetAll();
        Task<User> LoginAsync(User obj);
        //public Task<User> RegisterUserAsync(User user);
        //public Task<User> GetUserByAccessTokenAsync(string accessToken);
        //public Task<User> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}
