using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
	public class CharacteristicViewModel
	{
        public Guid? Id { get; set; }

        public int? No { get; set; }

        [Required]
        public string Name { get; set; }

        
        public Guid? StatusId { get; set; }
        public StatusItemViewModel? StatusItem { get; set; }
        public string StatusDisplay
        {
            get
            {
                return StatusItem != null ? StatusItem.Status : string.Empty;
            }
        }
        public List<SelectListItem>? Statuses { get; set; }

        public Guid? SpeciesId { get; set; }
        public SpeciesItemViewModel? SpeciesItem { get; set; }
        public string SpeciesDisplay
        {
            get
            {
                return SpeciesItem != null ? SpeciesItem.Species : string.Empty;
            }
        }
        public List<SelectListItem>? Specieses { get; set; }

        public Guid? TypeId { get; set; }
        public TypeItemViewModel? TypeItem { get; set; }
        public string TypeDisplay
        {
            get
            {
                return TypeItem != null ? TypeItem.Type : string.Empty;
            }
        }
        public List<SelectListItem>? Types { get; set; }

        public Guid? GenderId { get; set; }
        public GenderItemViewModel? GenderItem { get; set; }
        public string GenderDisplay
        {
            get
            {
                return GenderItem != null ? GenderItem.Gender : string.Empty;
            }
        }
        public List<SelectListItem>? Genders { get; set; }

        public Guid? LocationId { get; set; }
        public LocationViewModel? LocationItem { get; set; }
        public string LocationDisplay
        {
            get
            {
                return LocationItem != null ? LocationItem.Name : string.Empty;
            }
        }
        public List<SelectListItem>? Locations { get; set; }

        public Guid? OriginId { get; set; }
        public OriginItemViewModel? OriginItem { get; set; }
        public string OriginDisplay
        {
            get
            {
                return OriginItem != null ? OriginItem.Name : string.Empty;
            }
        }
        public List<SelectListItem>? Origins { get; set; }

        public string? Image { get; set; }

        public List<EpisodeViewModel>? Episode { get; set; }
        public string? EpisodeString { get; set; }

        public string? Url { get; set; }

        public DateTime Created { get; set; }
        public string CreatedDisplay {
            get
            {
                return Created.ToString("MM/dd/yyyy h:mm tt");
            }
        }
	} 
}

