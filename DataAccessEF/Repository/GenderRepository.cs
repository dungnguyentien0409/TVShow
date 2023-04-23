using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class GenderRepository : GenericRepository<GenderItem, TVShowContext>, IGenderRepository
    {
        public GenderRepository(TVShowContext context) : base(context)
        {
        }
    }
}

