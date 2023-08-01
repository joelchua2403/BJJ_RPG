using System;
using VS_RPG.DTO;
using VS_RPG.Models;

namespace VS_RPG.Services
{
	public interface IUserService
    {
        Task<ServiceResponse<int>> Register(RegisterUserDto RegisterUserDto);
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}

