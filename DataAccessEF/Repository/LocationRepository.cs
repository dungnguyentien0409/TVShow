using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(TVShowContext context) : base(context)
        {
        }
    }
}

