using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VS_RPG.Models;
using VS_RPG.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using VS_RPG.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace VS_RPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly DataContext _context;


        private ICharacterService _characterService;

        public CharacterController(DataContext context, ICharacterService characterService)
        {
            _context = context;
            _characterService = characterService;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Get()
        {
            ServiceResponse<List<Character>> response = new ServiceResponse<List<Character>>();
            List<Character> characters = await _context.Characters
                .Include(c => c.CharacterMoves)
                .ThenInclude(cm => cm.Move)
                .ToListAsync();
            response.Data = characters;
            return response;
        }


        [Authorize]
        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetOne(int Id)
        {
            return Ok(await _characterService.GetCharacterById(Id));

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Character>>> AddCharacter(Character newCharacter)
        {
            ServiceResponse<Character> response = new ServiceResponse<Character>();
            try
            {
                await _context.Characters.AddAsync(newCharacter);
                await _context.SaveChangesAsync();
                response.Data = newCharacter;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> UpdateCharacter(int id, Character updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(id, updatedCharacter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("{characterId}/moves")]
        public async Task<ActionResult<ServiceResponse<Character>>> AddCharacterMove(int characterId, int moveId)
        {
            var response = await _characterService?.AddCharacterMove(characterId, moveId);

            if (response == null || response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

    }
}