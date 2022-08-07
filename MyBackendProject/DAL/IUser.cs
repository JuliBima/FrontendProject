using MyBackendProject.DTO;

namespace MyBackendProject.DAL
{
    public interface IUser
    {
        Task Registration(CreateUserDto user);
        Task<UserDto> Authenticate(string username, string password);
        Task<IEnumerable<UserDto>> GetAll();
    }
}
