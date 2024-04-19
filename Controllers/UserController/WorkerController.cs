using Key_Management_System.DTOs.UserDto.WorkerDto;
using Key_Management_System.Services.UserService.WorkerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Key_Management_System.Controllers.UserController
{
    [Route("api/")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workService;

        public WorkerController(IWorkerService workerService)
        {
            _workService = workerService;
        }


        [HttpPost]
        [Route("register-worker")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterWorkerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _workService.RegisterWorker(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
