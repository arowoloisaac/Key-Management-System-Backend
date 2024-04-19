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
    public class UserService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtBearerTokenSettings _bearerTokenSettings;

        public UserService(UserManager<User> userManager, IOptions<JwtBearerTokenSettings> jwtTokenOptions)
        {
            _userManager = userManager;
            _bearerTokenSettings = jwtTokenOptions.Value;
        }

        public async Task<PublicUserModelDto> GetProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with email {email} does not found");
            }

            return new PublicUserModelDto
            {
                Email = user.Email,
                BirthDate = user.BirthDate,
                Name = user.Name
            };
        }

        public async Task<string> Login(LoginDto model)
        {
            var user = await ValidateUser(model);
            if (user == null)
            {
                throw new InvalidOperationException("Login failed");
            }

            return GenerateToken(user, await _userManager.IsInRoleAsync(user, ApplicationRoleNames.Administrator));
        }

        public async Task Register(UserCreateDto model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new ArgumentException("User with same email already exists");
            }

            var identityUser = new User
            {
                Name = model.Name,
                Email = model.Email,
                BirthDate = model.BirthDate,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
            {
                throw new Exception("Some errors during creating user");
            }
        }

        private async Task<User> ValidateUser(LoginDto credentials)
        {
            var identityUser = await _userManager.FindByEmailAsync(credentials.Email);
            if (identityUser != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash,
                    credentials.Password);
                return result == PasswordVerificationResult.Success ? identityUser : null;
            }

            return null;
        }

        private string GenerateToken(User user, bool isAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_bearerTokenSettings.SecretKey);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Authentication, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, isAdmin ? ApplicationRoleNames.Administrator : ApplicationRoleNames.User)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_bearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _bearerTokenSettings.Audience,
                Issuer = _bearerTokenSettings.Issuer,
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
