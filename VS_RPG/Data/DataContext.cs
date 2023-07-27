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
    }

	
}

