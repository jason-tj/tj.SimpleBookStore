using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tj.SimpleBookStore.Services.Interface;

namespace tj.SimpleBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet("token")]
        public IActionResult GetToken(string username)
        {
            var token = _jwtService.GenerateToken(username);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure()
        {
            return Ok("This is a secure endpoint.");
        }
    }
}
