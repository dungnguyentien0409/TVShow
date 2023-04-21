using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
	public class LocationsViewModel
	{
		[Display(Name="Locations")]
		public Guid? LocationId { get; set; }
		public List<SelectListItem> Locations { get; set; }

		public LocationsViewModel()
		{
			Locations = new List<SelectListItem>() {
				new SelectListItem()
				{
					Text = "--Select--",
					Value = Guid.Empty.ToString()
				},
				new SelectListItem()
				{
					Text = "null"
				}
			};
		}
	}
}

