using FrontendProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace FrontendProject.Services
{
    public class EnrollmentService : IEnrollment
    {
        public async Task Delete(int id,string token)
        {
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Enrollment/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll(string token)
        {
            List<Enrollment> results = new List<Enrollment>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Enrollment"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<Enrollment>>(apiResponse);

                }
            }
            return results;
        }

        public async Task<Enrollment> GetById(int id, string token)
        {
            Enrollment enrollment = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
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

        public async Task<Enrollment> Insert(Enrollment obj, string token)
        {
            Enrollment enrollment = new Enrollment();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
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

        public async Task<Enrollment> Update(Enrollment obj, string token)
        {
            Enrollment enrollment = await GetById(obj.EnrollmentID, token);
            if (enrollment == null)
                throw new Exception($"Data dengan id {obj.CourseID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PutAsync("https://localhost:6001/api/Enrollment", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    enrollment = JsonConvert.DeserializeObject<Enrollment>(apiResponse);
                }
            }
            return enrollment;
            throw new NotImplementedException();
        }
    }
}
