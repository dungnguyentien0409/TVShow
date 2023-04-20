using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("Characteristic")]
	public class Characteristic : EntityBase
	{
		public string Name { get; set; }
		public string Status { get; set; }
		public string Speices { get; set; }
		public string Type { get; set; }
		public string Gender { get; set; }

		public Characteristic() : base()
		{
		}
	}
}

