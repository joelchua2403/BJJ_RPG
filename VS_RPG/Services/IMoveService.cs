using System;
using System.Collections.Generic;
using VS_RPG.DTO;
using VS_RPG.Models;

namespace VS_RPG.Services
{
	public interface IMoveService
    {
        Task<ServiceResponse<MoveDto>> CreateMove(MoveDto newMoveDto);
        Task<ServiceResponse<List<MoveDto>>> GetAllMoves();
        Task<ServiceResponse<MoveDto>> GetMoveById(int id);
        Task<ServiceResponse<int>> DeleteMove(int id);
        //Task<ServiceResponse<MoveDto>> UpdateMove(int id, MoveDto updatedMoveDto);
    }
}

