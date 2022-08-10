using FrontendProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace FrontendProject.Services
{
    public class EnrollmentService : IEnrollment
    {
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Enrollment/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            List<Enrollment> results = new List<Enrollment>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Enrollment"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<Enrollment>>(apiResponse);

                }
            }
            return results;
        }

        public async Task<Enrollment> GetById(int id)
        {
            Enrollment enrollment = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Enrollment/{id}"))
                {
                    if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await respone.Content.ReadAsStringAsync();
                        enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                    }

                }
            }
            return enrollment;
        }

        public async Task<Enrollment> Insert(Enrollment obj)
        {
            Enrollment enrollment = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/Enrollment", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                    }
                }
            }
            return enrollment;
        }

        public async Task<Enrollment> Update(Enrollment obj)
        {
            Enrollment enrollment = await GetById(obj.EnrollmentID);
            if (enrollment == null)
                throw new Exception($"Data dengan id {obj.CourseID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:6001/api/Enrollment", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                }
            }
            return enrollment;
        }
    }
}
