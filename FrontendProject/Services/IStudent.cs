using FrontendProject.Models;

namespace FrontendProject.Services
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> GetByName(string fristName, string lastName);
        Task<IEnumerable<Student>> Pagging(int? skip, int? take);
    }
}
