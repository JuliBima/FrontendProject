using FrontendProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace FrontendProject.Services
{
    public class CourseService : ICourse
    {
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Course/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            List<Course> results = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Course"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<Course>>(apiResponse);

                }
            }
            return results;
        }

        public async Task<Course> GetById(int id)
        {
            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Course/{id}"))
                {
                    if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await respone.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }

                }
            }
            return course;
        }

        public async Task<IEnumerable<Course>> GetByTitle(string title)
        {
            List<Course> courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Course/ByTitle?title={title}"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);

                }
            }
            return courses;
        }

        public async Task<Course> Insert(Course obj)
        {
            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/Course", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        course = JsonConvert.DeserializeObject<Course>(apiResponse);
                    }
                }
            }
            return course;
        }

        public async Task<IEnumerable<Course>> Pagging(int? skip, int? take)
        {
        
            List<Course> courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Course/Pagging/{skip}/{take}"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);

                }
            }
            return courses;
        }

        public async Task<Course> Update(Course obj)
        {
            Course course = await GetById(obj.CourseID);
            if (course == null)
                throw new Exception($"Data dengan id {obj.CourseID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync("https://localhost:6001/api/Course", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    course = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return course;
        }
    }
}
