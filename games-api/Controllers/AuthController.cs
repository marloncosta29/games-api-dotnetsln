using GamesAPI.Context;
using GamesAPI.DTO;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controller
{
    [ApiController]
    [Route("auth")]
    public class AuthController(AuthService authService) : ControllerBase {
        private readonly AuthService _authService = authService;

        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDto data){
            var token = this._authService.Authenticate(data.Username, data.Password);
            if(token == null){
                return Unauthorized(new { message = "Username or password is incorrect"});
            }
            return Ok(new { token });

        }

    }
}