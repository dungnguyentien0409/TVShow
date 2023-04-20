using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace Domain.Entities
{
    [Table("Characteristic")]
    public class Characteristic : EntityBase
    {
        public string Name { get; set; }
        
        [ForeignKey("StatusItem")]
        public int? StatusId { get; set; }
        public StatusItem StatusItem { get; set; }

        [ForeignKey("SpeciesItem")]
        public int? SpeciesId { get; set; }
        public SpeciesItem SpeciesItem { get; set; }

        [ForeignKey("TypeItem")]
        public int? TypeId { get; set; }
        public TypeItem TypeItem { get; set; }

        [ForeignKey("GenderItem")]
        public int? GenderId { get; set; }
        public GenderItem GenderItem { get; set; }

        [ForeignKey("Origin")]
        public int? OriginId { get; set; }
        public Origin Origin { get; set; }

        [ForeignKey("Location")]
        public int? LocationId { get; set; }
        public Location Location { get; set; }
        
        public string Image { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public string Url { get; set; }

        public Characteristic(string name, string image, string url) : base()
        {
            Name = name;
            Image = image;
            Url = url;
            //Episodes = new List<Episode>();
        }
    }
}

