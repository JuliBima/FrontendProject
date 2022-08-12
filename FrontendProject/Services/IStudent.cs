using FrontendProject.Models;

namespace FrontendProject.Services
{
    public interface IStudent : ICrud<Student>
    {
        Task<IEnumerable<Student>> GetByName(string fristName, string lastName, string token);
        Task<IEnumerable<Student>> Pagging(int? skip, int? take , string token);

        Task<IEnumerable<StudentEnrollmentCourse>> GetEnrollmentCourses(string token);

    }
}
