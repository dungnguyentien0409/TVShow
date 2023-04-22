using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dto
{
	public class SearchCriteriaDto
	{
        public Guid? LocationId { get; set; }
        public List<SelectListItem> Locations { get; set; }

        public SearchCriteriaDto()
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

