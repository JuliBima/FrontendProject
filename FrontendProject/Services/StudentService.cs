using FrontendProject.Models;
using Newtonsoft.Json;

namespace FrontendProject.Services
{
    public class StudentService : IStudent
    {
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            List<Student> results = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Student"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<Student>>(apiResponse);

                }
            }
            return results;
        }

        public Task<Student> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Insert(Student obj)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(Student obj)
        {
            throw new NotImplementedException();
        }
    }
}
