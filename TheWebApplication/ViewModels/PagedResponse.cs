using System;
namespace ViewModels
{
	public class PagedResponse<T> where T : class
	{
		public int TotalPage { get; set; }
		public int PageIndex { get; set; }
		public T Results { get; set; }

		public PagedResponse()
		{
			Results = Activator.CreateInstance<T>();
		}
	}
}

