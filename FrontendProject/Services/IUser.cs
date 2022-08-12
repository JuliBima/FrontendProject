using FrontendProject.Models;

namespace FrontendProject.Services
{
    public interface IUser
    {
        Task Registration(User user);
        Task<UserRead> Authenticate(string username, string password);
        Task<IEnumerable<UserRead>> GetAll();
    }
}
