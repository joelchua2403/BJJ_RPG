using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VS_RPG.Models;

namespace VS_RPG.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> GetAllCharacters();
        Task<ServiceResponse<Character>> GetCharacterById(int id);
        Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter);
        Task<ServiceResponse<Character>> UpdateCharacter(Character updatedCharacter);
        Task<ServiceResponse<List<Character>>> DeleteCharacter(int id);
    }
}