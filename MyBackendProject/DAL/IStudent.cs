using MyBackendProject.Models;

namespace MyBackendProject.DAL
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> GetByName(string fristMidNamme, string lastName);

        Task<IEnumerable<Student>> Pagging(int skip, int take);

        Task<IEnumerable<Student>> GetEnrollmentCourses();
    }
}
