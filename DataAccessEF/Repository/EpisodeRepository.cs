﻿using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class EpisodeRepository : GenericRepository<Episode, TVShowContext>, IEpisodeRepository
    {
        public EpisodeRepository(TVShowContext context) : base(context)
        {
        }

        public IEnumerable<Episode> GetByCharacteristicId(Guid charId)
        {
            return context.Set<Episode>()
                .Where(w => w.CharacteristicId == charId)
                .OrderBy(o => o.Created);
        }
    }
}

