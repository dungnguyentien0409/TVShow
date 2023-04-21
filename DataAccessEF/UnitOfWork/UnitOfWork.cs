using System;
using DataAccessEF.Repository;
using Domain.Interfaces;

namespace DataAccessEF.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private TVShowContext context;

        public ICharacteristicRepository Characteristic { get; }
        public IEpisodeRepository Episode { get; }
        public IGenderRepository Gender { get; }
        public ILocationRepository Location { get; }
        public IOriginRepository Origin { get; }
        public ISpeciesRepository Species { get; }
        public IStatusRepository Status { get; }
        public ITypeRepository Type { get; }

        public UnitOfWork(TVShowContext context)
		{
			this.context = context;

			Characteristic = new CharacteristicRepository(context);
            Episode = new EpisodeRepository(context);
            Gender = new GenderRepository(context);
            Location = new LocationRepository(context);
            Origin = new OriginRepository(context);
            Species = new SpeciesRepository(context);
            Status = new StatusRepository(context);
            Type = new TypeRepository(context);
		}

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}

