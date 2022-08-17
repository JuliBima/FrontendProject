using FrontendProject.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace FrontendProject.Services
{
    public class UserService : IUser
    {
        public Task<UserRead> Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserRead>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> LoginAsync(User obj)
        {
            //    user.Password = Utility.Encrypt(user.Password);
            //    string serializedUser = JsonConvert.SerializeObject(user);

            //    var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/Login");
            //    requestMessage.Content = new StringContent(serializedUser);

            //    requestMessage.Content.Headers.ContentType
            //        = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //    var response = await _httpClient.SendAsync(requestMessage);

            //    var responseStatusCode = response.StatusCode;
            //    var responseBody = await response.Content.ReadAsStringAsync();

            //    var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            //    return await Task.FromResult(returnedUser);

            User student = new User();
            using (var httpClient = new HttpClient())
            {
                //Memasukan Token
                //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"{token}");
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:6001/api/User/Login", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        student = JsonConvert.DeserializeObject<User>(apiResponse);
                    }
                }
            }
            return student;
        }

        public Task Registration(User user)
        {
            throw new NotImplementedException();
        }
    }
}
