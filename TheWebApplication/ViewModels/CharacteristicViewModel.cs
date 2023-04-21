using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
	public class CharacteristicViewModel
	{
        public Guid Id { get; set; }

        [Display(Name = "No")]
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
        public LocationViewModel Location { get; set; }

        public Guid? LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        [Display(Name = "Origin")]
        public OriginViewModel Origin { get; set; }

        public Guid? OriginId { get; set; }
        public IEnumerable<SelectListItem> Origins { get; set; }

        [Display(Name = "Image")]
        public string? Image { get; set; }

        [Display(Name = "Episode")]
        public ICollection<EpisodeViewModel> Episode { get; set; }
        public string? EpisodeString { get; set; }

        [Display(Name = "Url")]
        public string? Url { get; set; }

        public DateTime Created { get; set; }

        [Display(Name = "DateTime")]
        public string CreatedDisplay { get; set; }

        public CharacteristicViewModel()
        {
            Id = Guid.NewGuid();
            Locations = new List<SelectListItem>();
        }
	}

	public class LocationViewModel
	{
        public Guid Id { get; set; }
		public string Name { get; set; }
		public string Url { get; set; }
	}

	public class OriginViewModel
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
		public string Url { get; set; }
	}

    public class EpisodeViewModel
    {
        public string EpisodeUrl { get; set; }
    }
}

