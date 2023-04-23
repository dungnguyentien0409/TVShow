using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class StatusRepository : GenericRepository<StatusItem, TVShowContext>, IStatusRepository
    {
		public StatusRepository(TVShowContext context) : base(context)
        {
		}

		public void RemoveAll()
		{

		}
	}
}

