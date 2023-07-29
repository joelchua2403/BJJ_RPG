using System;
namespace VS_RPG.Models
{
    public class CharacterMove
    {
        public int CharacterId { get; set; }
        public Character? Character { get; set; }

        public int MoveId { get; set; }
        public Move? Move { get; set; }

        // If you want to add additional properties to the relationship, you can add them here
    }


}