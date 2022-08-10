using MyBackendProject.Models;

namespace MyBackendProject.DAL
{
    public interface IEnrollment : ICrud<Enrollment>
    {
        Task<Enrollment> InsertEnrollment(Enrollment obj, int studentID, int courseID);
        Task<IEnumerable<Enrollment>> GetEnrollmentStudentCourses();

    }
}
