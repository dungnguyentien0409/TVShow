using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("StatusItem")]
	public class StatusItem : EntityBase
	{
		public string Status { get; set; }

		public StatusItem(string status)
		{
			Status = status;
		}
	}
}

