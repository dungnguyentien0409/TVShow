﻿using System;
namespace Domain.Entities
{
	public abstract class EntityBase
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }

		public EntityBase()
		{
			Id = Guid.NewGuid();
			Created = DateTime.Now;
		}
	}
}

