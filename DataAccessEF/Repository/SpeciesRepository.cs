using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class SpeciesRepository : GenericRepository<SpeciesItem, TVShowContext>, ISpeciesRepository
    {
        public SpeciesRepository(TVShowContext context) : base(context)
        {
        }
    }
}

