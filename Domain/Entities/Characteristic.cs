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
        public Guid? StatusId { get; set; }
        public StatusItem StatusItem { get; set; }

        [ForeignKey("SpeciesItem")]
        public Guid? SpeciesId { get; set; }
        public SpeciesItem SpeciesItem { get; set; }

        [ForeignKey("TypeItem")]
        public Guid? TypeId { get; set; }
        public TypeItem TypeItem { get; set; }

        [ForeignKey("GenderItem")]
        public Guid? GenderId { get; set; }
        public GenderItem GenderItem { get; set; }

        [ForeignKey("Origin")]
        public Guid? OriginId { get; set; }
        public Origin Origin { get; set; }

        [ForeignKey("Location")]
        public Guid? LocationId { get; set; }
        public Location Location { get; set; }
        
        public string Image { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public string Url { get; set; }
    }
}

