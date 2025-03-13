using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using tj.SimpleBookStore.DbContexts;
using tj.SimpleBookStore.Models;
using tj.SimpleBookStore.Repository;
using tj.SimpleBookStore.Services.Interface;
using tj.SimpleBookStore.Unit;

namespace tj.SimpleBookStore.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserContext _userContext;

        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository, UserContext userContext)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _userContext = userContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GenerateToken(string username)
        {
            var user = _userRepository.GetUserByUserNameAsync(username).Result;
            if (user == null)
            {
                throw new KeyNotFoundException("user not exist");
            }
            _userContext.CurrentUser = new UserInfo
            {
                UserId = user.Id,
                Username = user.UserName ?? "",
                Role = user.UserName ?? ""
            };
            return JwtTokenHelper.GenerateToken(username, username, _configuration["Jwt:Key"], _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"]);
        }
    }
}
