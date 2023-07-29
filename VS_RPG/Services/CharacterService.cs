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

    public class CharacterService : ICharacterService
    {

        private readonly DataContext _context;



        public CharacterService(DataContext context)
        {
            _context = context;
        }




        //public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        //{
        //    var serviceResponse = new ServiceResponse<List<Character>>();
        //    var dbCharacters = await _context.Characters.ToListAsync();
        //    var id = dbCharacters.Max(c => c.Id) + 1;
        //    newCharacter.Id = id;
        //    dbCharacters.Add(newCharacter);
        //    serviceResponse.Data = dbCharacters;
        //    return serviceResponse;
        //}

        public async Task<ServiceResponse<Character>> AddCharacter(Character newCharacter)
        {
            ServiceResponse<Character> response = new ServiceResponse<Character>();
            try
            {
                var moves = new List<CharacterMove>();
                foreach (var move in newCharacter.CharacterMoves)
                {
                    var dbMove = await _context.Moves.FirstOrDefaultAsync(m => m.Id == move.MoveId);
                    if (dbMove != null)
                    {
                        moves.Add(new CharacterMove
                        {
                            Move = dbMove
                        });
                    }
                }

                newCharacter.CharacterMoves = moves;

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
                var existingCharacter = await _context.Characters.FindAsync(updatedCharacter.Id);

                if (existingCharacter != null)
                {
                    _context.Entry(existingCharacter).CurrentValues.SetValues(updatedCharacter);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = updatedCharacter;
                    serviceResponse.Message = "Character updated successfully";
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Message = "Character not found";
                    serviceResponse.Success = false;
                }
            }
            catch (Exception e)
            {
                serviceResponse.Message = $"Character update failed: {e.Message}";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<bool>();
            try
            {
                var character = await _context.Characters.Include(c => c.CharacterMoves).FirstOrDefaultAsync(c => c.Id == id);
                if (character != null)
                {
                    foreach (var characterMove in character.CharacterMoves)
                    {
                        _context.CharacterMoves.Remove(characterMove);
                    }
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = true;
                    serviceResponse.Message = "Character deleted successfully";
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Message = "Character not found";
                    serviceResponse.Success = false;
                }
            }
            catch (Exception e)
            {
                serviceResponse.Message = $"Character deletion failed: {e.Message}, Inner Exception: {e.InnerException?.Message}";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }


        public async Task<ServiceResponse<CharacterMove>> AddCharacterMove(int characterId, int moveId)
        {
            ServiceResponse<CharacterMove> response = new ServiceResponse<CharacterMove>();
            try
            {
                Character character = await _context.Characters
                    .Include(c => c.CharacterMoves)
                    .FirstOrDefaultAsync(c => c.Id == characterId);

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                Move move = await _context.Moves.FirstOrDefaultAsync(m => m.Id == moveId);

                if (move == null)
                {
                    response.Success = false;
                    response.Message = "Move not found.";
                    return response;
                }

                CharacterMove characterMove = new CharacterMove
                {
                    Character = character,
                    Move = move
                };

                await _context.CharacterMoves.AddAsync(characterMove);
                await _context.SaveChangesAsync();

                response.Data = characterMove;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;

        }


    }
    }