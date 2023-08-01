using System;
using Microsoft.AspNetCore.Mvc;
using VS_RPG.DTO;
using VS_RPG.Models;
using VS_RPG.Services;

namespace VS_RPG.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
		{
            ServiceResponse<int> response = await _userService.Register(
            registerUserDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			ServiceResponse<string> response = await _userService.Login(
				loginDto.Username, loginDto.Password);
			if (!response.Success)
			{
				return BadRequest(response);
			}
			return Ok(response);
		}
    }
	
}

