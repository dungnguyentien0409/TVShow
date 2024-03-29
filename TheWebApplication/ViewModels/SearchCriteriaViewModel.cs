﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
	public class SearchCriteriaViewModel
	{
		[Display(Name="Locations")]
		public Guid? LocationId { get; set; }
		public List<SelectListItem> Locations { get; set; }
	}
}

