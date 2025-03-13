using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tj.SimpleBookStore.Services.Interface;

namespace tj.SimpleBookStore.Controllers
{
    /// <summary>
    /// 验证控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenService;

        public AuthController(IAuthenticationService authenService)
        {
            _authenService = authenService;
        }

        /// <summary>
        /// 获取用户TOKEN
        /// 预设 general/admin 两个用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("token")]
        public IActionResult GetToken(string username)
        {
            var token = _authenService.GenerateToken(username);
            return Ok(new { Token = token });
        }
    }
}
