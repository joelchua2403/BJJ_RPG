using System;
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
