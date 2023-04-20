using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class GenderRepository : GenericRepository<GenderItem>, IGenderRepository
    {
        public GenderRepository(TVShowContext context) : base(context)
        {
        }
    }
}

