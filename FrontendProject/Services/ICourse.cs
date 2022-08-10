using FrontendProject.Models;

namespace FrontendProject.Services
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetByTitle(string title);
        Task<IEnumerable<Course>> Pagging(int? skip, int? take);
    }
}
