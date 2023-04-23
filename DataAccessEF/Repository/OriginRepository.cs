using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class OriginRepository : GenericRepository<Origin, TVShowContext>, IOriginRepository
    {
        public OriginRepository(TVShowContext context) : base(context)
        {
        }
    }
}

