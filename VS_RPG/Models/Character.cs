using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VS_RPG.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Joel";
        public int HitPoints { get; set; } = 20;

        public int Strength { get; set; } = 10;

        public int Skill { get; set; } = 10;

        public int Agility { get; set; } = 10;

        public List<CharacterMove> CharacterMoves { get; set; }

        public RpgClass Class { get; set; } = RpgClass.Scrambler;
     
    }
}