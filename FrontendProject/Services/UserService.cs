using FrontendProject.Models;

namespace FrontendProject.Services
{
    public class UserService : IUser
    {
        public Task<UserRead> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRead>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Registration(User user)
        {
            throw new NotImplementedException();
        }
    }
}
