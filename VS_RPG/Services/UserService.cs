using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VS_RPG.Data;
using VS_RPG.DTO;
using VS_RPG.Models;

namespace VS_RPG.Services
{
	public class UserService : IUserService
	{
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;


        // Helper methods
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }

            return false;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        // Constructor
        public UserService(DataContext context, IConfiguration configuration)
		{
            _configuration = configuration;
            _context = context;
		}


        // Login Method
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new ServiceResponse<string>
                {
                    Success = false,
                    Message = "Wrong password."
                };
            }
            return new ServiceResponse<string>
            {
                Data = GenerateJwtToken(user)
            };
        }

        //Register Method
        public async Task<ServiceResponse<int>> Register(RegisterUserDto RegisterUserDto)
        {
            if (await UserExists(RegisterUserDto.Username))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }
            var user = new User
            {
                Username = RegisterUserDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(RegisterUserDto.Password)
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int>
            {
                Data = user.Id,
                Message = "Registration Successful."
            };
        }
    }
}

