using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataAccessEF
{
	public class TVShowContext : DbContext
	{
		public TVShowContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Characteristic> Characteristic { get; set; }
    }
}

