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


namespace VS_RPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {


        
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id=1, Name = "Gordan Ryan" }
        };

        private ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Get()
        {
            return Ok(await characterService.GetAllCharacters());
        }

    

        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetOne(int Id)
        {
            return Ok(await characterService.GetCharacterById(Id));
   
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character newCharacter)
    {
        return Ok(await characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Character>>> UpdateCharacter(Character updatedCharacter)
    {
        var response = await characterService.UpdateCharacter(updatedCharacter);
        if (response.Data == null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

     [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> DeleteCharacter(int id)
    {
        var response = await characterService.DeleteCharacter(id);
        if (response.Data == null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}

   

}