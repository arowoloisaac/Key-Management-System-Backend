using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Configuration;
using WebApplication2.DTO;
using WebApplication2.Services;

namespace WebApplication2.Controllers
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
        public async Task<IActionResult> Login([FromBody] LoginCredentials model)
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
