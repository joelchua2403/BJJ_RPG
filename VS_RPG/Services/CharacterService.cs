using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VS_RPG.Controllers;
using VS_RPG.Data;
using VS_RPG.Models;

namespace VS_RPG.Services
{
    
    public class CharacterService: ICharacterService
    {

        private readonly DataContext _context;

        public CharacterService(DataContext context)
        {
            _context = context;
        }
    
        private static List<Move> defaultGuardPasserMoves = new List<Move>()
        {
            new Move(1, "Double Leg", 2),
            new Move(2, "Single Leg", 2),
            new Move(3, "Knee Slide", 2),
            new Move(4, "Knee Cut", 2),
            new Move(5, "Full Mount", 4),
        };

        private static List<Move> defaultScramblerMoves = new List<Move>()
        {
            new Move(1, "Double Leg", 2),
            new Move(2, "Single Leg", 2),
            new Move(6, "Gullotine", 10),
            new Move(7, "Cartwheel Pass", 3),
            new Move(8, "Full Mount", 4),
        };

        private static List<Character> characters = new List<Character> {
            new Character {Id = 1, Name = "Joel" , Class = RpgClass.GuardPasser, Moveset = defaultGuardPasserMoves },
            new Character {Id = 2, Name = "Ellie" , Class = RpgClass.Scrambler, Moveset = defaultScramblerMoves },

        
        };
        

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            var id = characters.Max(c => c.Id) + 1;
            newCharacter.Id = id;
            characters.Add(newCharacter);
            serviceResponse.Data = characters;
            return serviceResponse;
        }



        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = dbCharacter;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> UpdateCharacter(Character updatedCharacter)
        {

            var serviceResponse = new ServiceResponse<Character>();
            try
            {
            
            var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
            if (character == null)
            {
                throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
            }
            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Skill = updatedCharacter.Skill;
            character.Agility = updatedCharacter.Agility;
            character.Class = updatedCharacter.Class;
            character.Moveset = updatedCharacter.Moveset;
            serviceResponse.Data = character;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            try
            {
                var character = characters.First(c => c.Id == id);
                if (character == null)
                {
                    throw new Exception($"Character with Id '{id}' not found");
                }
                characters.Remove(character);
                serviceResponse.Data = characters;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }
        
   
    }
}