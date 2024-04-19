using System.Security.Claims;
using Key_Management_System.DTOs.UserDto.SharedDto;
using Key_Management_System.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Key_Management_System.Services;

namespace Key_Management_System.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUsersService _usersService;
        private ITokenStorageService _tokenStorageService;

        public AuthController(IUsersService usersService, ITokenStorageService tokenStorageService)
        {
            _usersService = usersService;
            _tokenStorageService = tokenStorageService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                return Ok(await _usersService.Login(model));
            }
            catch (InvalidOperationException ex)
            {
                // Write logs
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return BadRequest();
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.Authentication).Value);
            _tokenStorageService.LogoutToken(id);
            return Ok();
        }
    }
}
