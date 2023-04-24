using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dto
{
	public class CharacteristicDto
	{
        public Guid Id { get; set; }

        public int No { get; set; }

        public string Name { get; set; }

        public Guid? StatusId { get; set; }
        public StatusItemDto? StatusItem { get; set; }
        public List<SelectListItem> Statuses { get; set; }

        public Guid? SpeciesId { get; set; }
        public SpeciesItemDto? SpeciesItem { get; set; }
        public List<SelectListItem> Specieses { get; set; }

        public Guid? TypeId { get; set; }
        public TypeItemDto? TypeItem { get; set; }
        public List<SelectListItem> Types { get; set; }

        public Guid? GenderId { get; set; }
        public GenderItemDto? GenderItem { get; set; }
        public List<SelectListItem> Genders { get; set; }

        public Guid? LocationId { get; set; }
        public LocationDto? LocationItem { get; set; }
        public List<SelectListItem> Locations { get; set; }

        public Guid? OriginId { get; set; }
        public OriginDto? OriginItem { get; set; }
        public List<SelectListItem> Origins { get; set; }

        public string? Image { get; set; }

        public ICollection<EpisodeDto> Episodes { get; set; }
        public string? EpisodeString { get; set; }

        public string? Url { get; set; }

        public DateTime Created { get; set; }

        public CharacteristicDto()
        {
            Statuses = InitDropDownValue();
            Specieses = InitDropDownValue();
            Types = InitDropDownValue();
            Genders = InitDropDownValue();
            Locations = InitDropDownValue();
            Origins = InitDropDownValue();
            Url = "";
            Image = "";
        }

        private List<SelectListItem> InitDropDownValue()
        {
            var res = new List<SelectListItem>() {
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

            return res;
        }
	}
}

