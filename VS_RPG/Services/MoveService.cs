using System;
using Microsoft.EntityFrameworkCore;
using VS_RPG.Data;
using VS_RPG.DTO;
using VS_RPG.Models;

namespace VS_RPG.Services
{
	public class MoveService : IMoveService
	{
		
			private readonly DataContext _context;

		public MoveService(DataContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<MoveDto>> CreateMove(MoveDto newMoveDto)
		{
            ServiceResponse<MoveDto> response = new ServiceResponse<MoveDto>();
            try
            {
                Move move = new Move
                {
                    Name = newMoveDto.Name,
                    Points = newMoveDto.Points
                };

                await _context.Moves.AddAsync(move);
                await _context.SaveChangesAsync();

                response.Data = new MoveDto
                {
                    Id = move.Id,
                    Name = move.Name,
                    Points = move.Points
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;
		}

        public Task<ServiceResponse<MoveDto>> DeleteMove(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<MoveDto>>> GetAllMoves()
        {
            ServiceResponse<List<MoveDto>> response = new ServiceResponse<List<MoveDto>>();
            try
            {
                List<Move> dbMoves = await _context.Moves.ToListAsync();

                response.Data = dbMoves.Select(m => new MoveDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Points = m.Points
                }).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

            public Task<ServiceResponse<MoveDto>> GetMoveById(int id)
        {
            throw new NotImplementedException();
        }

        //public Task<ServiceResponse<MoveDto>> UpdateMove(int id, Move updatedMoveDto)
        //{
        //    throw new NotImplementedException();
        //}
    }
	}


