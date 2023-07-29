using System;
using Microsoft.EntityFrameworkCore;
using VS_RPG.Models;

namespace VS_RPG.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Move> Moves { get; set; }
        public DbSet<CharacterMove> CharacterMoves { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterMove>()
                .HasKey(cm => new { cm.CharacterId, cm.MoveId });

            // Your other model configurations...
        }


    }


}

