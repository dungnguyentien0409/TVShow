using System;
namespace Models
{
	public class RemoteDataResponse
	{
		public Info Info { get; set; }
		public List<Characteristic> Results { get; set; }

		public RemoteDataResponse()
		{
			Info = new Info();
			Results = new List<Characteristic>();
		}
	}

	public class Info
	{
		public int Count { get; set; }
		public int Pages { get; set; }
		public string Next { get; set; }
		public string Prev { get; set; }
	}
}

