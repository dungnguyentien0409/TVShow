using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(TVShowContext context) : base(context)
        {
        }
    }
}

