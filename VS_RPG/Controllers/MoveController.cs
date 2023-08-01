using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS_RPG.Data;
using VS_RPG.DTO;
using VS_RPG.Models;
using VS_RPG.Services;

namespace VS_RPG.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MoveController : ControllerBase
	{
		private readonly DataContext _context;
		private IMoveService _moveService;

		public MoveController(DataContext context, IMoveService moveService)
		{
			_context = context;
			_moveService = moveService;
		}

        [Authorize]
        [HttpPost]
		public async Task<ActionResult<ServiceResponse<MoveDto>>> CreateMove(MoveDto newMove)
		{
			var response = await _moveService.CreateMove(newMove);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);


		}

        [Authorize]
        [HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<MoveDto>>> GetOneMove(int id)
		{
			return Ok(await _moveService.GetMoveById(id));
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<MoveDto>>>> GetAllMoves()
		{
			var response = await _moveService.GetAllMoves();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);


        }

        [Authorize]
        [HttpPut("{id}")]
		public async Task<ActionResult<ServiceResponse<MoveDto>>> UpdateMove(int id, MoveDto updatedMoveDto)
		{
			return Ok(await _moveService.UpdateMove(id, updatedMoveDto));
		}

        [Authorize]
        [HttpDelete]
		public async Task<ActionResult<ServiceResponse<int>>> DeleteMove(int id)
		{
			var response = await _moveService.DeleteMove(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }

}
