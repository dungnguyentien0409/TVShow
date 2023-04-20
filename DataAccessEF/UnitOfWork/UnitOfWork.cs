using System;
using DataAccessEF.Repository;
using Domain.Interfaces;

namespace DataAccessEF.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private TVShowContext context;

		public ICharacteristicRepository Characteristic { get; }

        public UnitOfWork(TVShowContext context)
		{
			this.context = context;

			this.Characteristic = new CharacteristicRepository(context);
		}

        public void Dispose()
        {
            context.Dispose();
        }

        public int Save()
        {
            return context.SaveChanges();
        }
    }
}

