using System;
using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IEpisodeRepository : IGenericRepository<Episode>
	{
        IEnumerable<Episode> GetByCharacteristicId(Guid charId);
    }
}

