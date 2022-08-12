namespace FrontendProject.Services
{
    public interface ICrud <T>
    {
        Task<IEnumerable<T>> GetAll(string token);
        Task<T> GetById(int id, string token);
        Task<T> Insert(T obj, string token);
        Task<T> Update(T obj, string token);
        Task Delete(int id, string token);
    }
}
