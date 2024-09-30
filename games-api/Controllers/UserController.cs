using GamesAPI.DTO;
using GamesAPI.Entities;
using GamesAPI.Services;
using Microsoft.AspNetCore.Mvc;




namespace GamesAPI.Controller
{
    [ApiController]
    [Route("users")]
    public class UserController(UserService userService) : ControllerBase {
        private readonly UserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto data){
            try
            {
                UserEntity user = await this._userService.Create(data);
                return CreatedAtAction(nameof(Create), new { id = user.Id }, new { id = user.Id } );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}