using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dto
{
	public class CharacteristicDto
	{
        public Guid Id { get; set; }

        public int No { get; set; }

        [Required]
		[Display(Name="Name")]
        public string Name { get; set; }

		[Display(Name="Status")]
        public string Status { get; set; }

        public Guid? StatusId { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }

		[Display(Name="Species")]
		public string Species { get; set; }

        public Guid? SpeciesId { get; set; }
        public IEnumerable<SelectListItem> Specieses { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        public Guid? TypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        public Guid? GenderId { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }

        [Display(Name = "Location")]
        public LocationDto Location { get; set; }

        public Guid? LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        [Display(Name = "Origin")]
        public OriginDto Origin { get; set; }

        public Guid? OriginId { get; set; }
        public IEnumerable<SelectListItem> Origins { get; set; }

        [Display(Name = "Image")]
        public string? Image { get; set; }

        [Display(Name = "Episode")]
        public ICollection<EpisodeDto> Episode { get; set; }
        public string? EpisodeString { get; set; }

        [Display(Name = "Url")]
        public string? Url { get; set; }

        public DateTime Created { get; set; }

        [Display(Name = "DateTime")]
        public string CreatedDisplay { get; set; }

        public CharacteristicDto()
        {
            Id = Guid.NewGuid();
            Locations = new List<SelectListItem>();
        }
	}

	public class LocationDto
	{
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
	}

	public class OriginDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public string Url { get; set; }
	}

    public class EpisodeDto
    {
        public string EpisodeUrl { get; set; }
    }
}

