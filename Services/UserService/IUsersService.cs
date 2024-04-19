using Key_Management_System.DTOs.UserDto.SharedDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Key_Management_System.Configuration;
using Key_Management_System.DTO;
using Key_Management_System.Models;

namespace Key_Management_System.Services.UserService
{
    public interface IUsersService
    {
        Task Register(UserCreateDto model);
        Task<PublicUserModelDto> GetProfile(string email);
        Task<string> Login(LoginDto model);
    }
}
