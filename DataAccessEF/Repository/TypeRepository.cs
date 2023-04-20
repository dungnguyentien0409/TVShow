using System;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccessEF.Repository
{
	public class TypeRepository : GenericRepository<TypeItem>, ITypeRepository
    {
        public TypeRepository(TVShowContext context) : base(context)
        {
        }
    }
}

