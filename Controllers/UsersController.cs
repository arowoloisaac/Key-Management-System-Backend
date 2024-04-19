using System.Security.Claims;
using Key_Management_System.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Key_Management_System.Configuration;
using Key_Management_System.DTO;

namespace Key_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UserCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.Register(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return BadRequest();
        }

        [HttpGet("profile")]
        [Authorize(Policy = ApplicationRoleNames.Administrator)]
        public async Task<ActionResult<PublicUserModelDto>> Get()
        {
            try
            {
                var emailClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
                return await _userService.GetProfile(emailClaim.Value);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // logs
            }

            return BadRequest();
        }

    }
}
