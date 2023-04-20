using System;
namespace Models
{
	public class Response
	{
		public Info Info { get; set; }
		public List<Characteristic> Results { get; set; }
	}

	public class Info
	{
		public int Count { get; set; }
		public int Pages { get; set; }
		public string Next { get; set; }
		public string Prev { get; set; }
	}
}

