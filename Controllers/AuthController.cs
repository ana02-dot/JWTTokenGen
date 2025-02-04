using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebToken.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtService = new JWTService();

        [HttpPost]
        public IActionResult Login([FromBody] LoginInfo loginInfo)
        {
            if (loginInfo.UserName == "ana" && loginInfo.Password == "def@123")
            {
                var token = _jwtService.GenerateToken(loginInfo.UserName);
                return Ok(new { Token = token});
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetSecuredData()
        {
            return Ok("Protected data");
        }

        public class LoginInfo()
        {
            public string ?UserName { get; set; }
            public string ?Password { get; set; }

        }
    }
}
