using FrontendProject.Models;

namespace FrontendProject.Services
{
    public interface IEnrollment : ICrud<Enrollment>
    {
        Task<IEnumerable<Enrollment>> Pagging(int? skip, int? take, string token);
    }
}
