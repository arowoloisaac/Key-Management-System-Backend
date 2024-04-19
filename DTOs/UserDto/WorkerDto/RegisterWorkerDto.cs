using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;

namespace Key_Management_System.DTOs.UserDto.WorkerDto
{
    public class RegisterWorkerDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = "example@example.com";

        [Phone(ErrorMessage ="Must be a valid number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Faculty { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
