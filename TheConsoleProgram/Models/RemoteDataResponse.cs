using System;
namespace Models
{
	public class RemoteDataResponse<T> where T : class
	{
		public Info Info { get; set; }
		public List<T> Results { get; set; }

		public RemoteDataResponse()
		{
			Info = new Info();
			Results = new List<T>();
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

