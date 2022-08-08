using MyBackendProject.Models;

namespace MyBackendProject.DAL
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetByTitle(string title);

        Task<IEnumerable<Course>> Pagging(int skip, int take);
    }
}
