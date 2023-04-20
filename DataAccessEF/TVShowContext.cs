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

		public DbSet<Characteristic> Characteristics { get; set; }
		public DbSet<Episode> Episodes { get; set; }
		public DbSet<GenderItem> Genders { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<Origin> Origins { get; set; }
		public DbSet<SpeciesItem> Species { get; set; }
		public DbSet<StatusItem> Statuses { get; set; }
		public DbSet<TypeItem> Types { get; set; }
    }
}

