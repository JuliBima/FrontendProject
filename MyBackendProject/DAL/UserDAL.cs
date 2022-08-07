using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyBackendProject.DTO;
using MyBackendProject.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBackendProject.DAL
{
    public class UserDAL : IUser
    {
        private UserManager<IdentityUser> _userManager;
        private AppSettings _appSettings;

        public UserDAL(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;

        }
        public async Task<UserDto> Authenticate(string username, string password)
        {
            var currUser = await _userManager.FindByNameAsync(username);
            var userResult = await _userManager.CheckPasswordAsync(currUser, password);
            if (!userResult)
                throw new Exception("Authenticate Gagal !");

            var user = new UserDto
            {
                Username = username
            };
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

            var tokenHendler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHendler.CreateToken(tokenDescriptor);
            user.Token = tokenHendler.WriteToken(token);
            return user;
        }

        public Task<IEnumerable<UserDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Registration(CreateUserDto user)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Username,
                    Email = user.Username
                };
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!result.Succeeded)
                {
                    StringBuilder sb = new StringBuilder();
                    var errors = result.Errors;
                    foreach (var error in errors)
                    {
                        sb.Append($"{error.Code} - {error.Description} \n");
                    }
                    throw new Exception($"Error : {sb.ToString()}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error : {ex.Message}");
            }
        }
    }
}
