using FrontendProject.Models;

namespace FrontendProject.Services
{
    public interface ICourse : ICrud<Course>
    {
        Task<IEnumerable<Course>> GetByTitle(string title, string token);
        Task<IEnumerable<Course>> Pagging(int? skip, int? take, string token);
        Task<IEnumerable<CourseElementStudent>> GetWithStudent(string token);
    }
}
