using FrontendProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace FrontendProject.Services
{
    public class CourseService : ICourse
    {
        public async Task Delete(int id, string token)
        {
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Course/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }

        public async Task<IEnumerable<Course>> GetAll(string token)
        {
            List<Course> results = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Course"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<Course>>(apiResponse);

                }
            }
            return results;
        }

        public async Task<Course> GetById(int id, string token)
        {
            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
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

        public async Task<IEnumerable<Course>> GetByTitle(string title, string token)
        {
            List<Course> courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Course/ByTitle?title={title}"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);

                }
            }
            return courses;
        }

        public async Task<IEnumerable<CourseElementStudent>> GetWithStudent(string token)
        {
            List<CourseElementStudent> results = new List<CourseElementStudent>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Course/WithEnrollmentStudent"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<CourseElementStudent>>(apiResponse);

                }
            }
            return results;
        }

        public async Task<Course> Insert(Course obj, string token)
        {
            Course course = new Course();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
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

        public async Task<IEnumerable<Course>> Pagging(int? skip, int? take, string token)
        {
        
            List<Course> courses = new List<Course>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Course/Pagging/{skip}/{take}"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    courses = JsonConvert.DeserializeObject<List<Course>>(apiResponse);

                }
            }
            return courses;
        }

        public async Task<Course> Update(Course obj, string token)
        {
            Course course = await GetById(obj.CourseID,token);
            if (course == null)
                throw new Exception($"Data dengan id {obj.CourseID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PutAsync("https://localhost:6001/api/Course", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    course = JsonConvert.DeserializeObject<Course>(apiResponse);
                }
            }
            return course;
            //throw new NotImplementedException();
        }
    }
}
