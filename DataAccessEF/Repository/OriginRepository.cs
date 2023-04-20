using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class OriginRepository : GenericRepository<Origin>, IOriginRepository
    {
        public OriginRepository(TVShowContext context) : base(context)
        {
        }
    }
}

