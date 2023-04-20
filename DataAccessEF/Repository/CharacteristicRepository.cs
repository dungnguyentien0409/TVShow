using System;
using System.Net;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class CharacteristicRepository : GenericRepository<Characteristic>, ICharacteristicRepository
    {
		public CharacteristicRepository(TVShowContext context) : base(context)
		{
		}
	}
}

