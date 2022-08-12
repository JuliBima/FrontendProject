using FrontendProject.Models;
using Newtonsoft.Json;

using System.Text;


namespace FrontendProject.Services
{
    public class StudentService : IStudent
    {

        public async Task Delete(int id, string token)
        {
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.DeleteAsync($"https://localhost:6001/api/Student/{id}"))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new Exception($"Gagal untuk delete data");
                }
            }
        }



        public async Task<IEnumerable<Student>> GetAll(string token)
        {
            List<Student> results = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Student"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<Student>>(apiResponse);

                }
            }
            return results;
        }


        public async Task<Student> GetById(int id, string token)
        {
            Student student = new Student();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");

                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Student/{id}"))
                {
                    if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await respone.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }

                }
            }
            return student;
        }

        public async Task<IEnumerable<Student>> GetByName(string fristName, string lastName, string token)
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Student/ByName?fristMidName={fristName}&lastName={lastName}"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);

                }
            }
            return students;
        }

        public async Task<IEnumerable<StudentEnrollmentCourse>> GetEnrollmentCourses(string token)
        {
            List<StudentEnrollmentCourse> results = new List<StudentEnrollmentCourse>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync("https://localhost:6001/api/Student/WithEnrollmentCourses"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    results = JsonConvert.DeserializeObject<List<StudentEnrollmentCourse>>(apiResponse);

                }
            }
            return results;
        }
    

        public async Task<Student> Insert(Student obj, string token)
        {
            Student student = new Student();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/Student", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<Student>(apiResponse);
                    }
                }
            }
            return student;
        }

        public async Task<IEnumerable<Student>> Pagging(int? skip, int? take, string token)
        {
            List<Student> students = new List<Student>();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var respone = await httpClient.GetAsync($"https://localhost:6001/api/Student/Pagging/{skip}/{take}"))
                {
                    string apiResponse = await respone.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);

                }
            }
            return students;
        }

        public async Task<Student> Update(Student obj, string token)
        {
            Student student = await GetById(obj.ID,token);
            if (student == null)
                throw new Exception($"Data dengan id {obj.ID} tidak ditemukan");
            StringContent content = new StringContent(JsonConvert.SerializeObject(obj),
                  Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                using (var response = await httpClient.PutAsync("https://localhost:6001/api/Student", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<Student>(apiResponse);
                }
            }
            return student;
            //throw new NotImplementedException();


        }

    }
}