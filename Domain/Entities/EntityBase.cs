using System;
namespace Domain.Entities
{
	public abstract class EntityBase
	{
		public int Id { get; set; }
		public DateTime Created { get; set; }

		public EntityBase()
		{
			Created = DateTime.Now;
		}
	}
}

