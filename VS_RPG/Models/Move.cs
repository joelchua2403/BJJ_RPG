using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VS_RPG.Models
{


    public class Move
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public List<CharacterMove> CharacterMoves { get; set; }

    
}

}
           
             