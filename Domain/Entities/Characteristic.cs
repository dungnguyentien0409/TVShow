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
        public virtual StatusItem StatusItem { get; set; }

        [ForeignKey("SpeciesItem")]
        public Guid? SpeciesId { get; set; }
        public virtual SpeciesItem SpeciesItem { get; set; }

        [ForeignKey("TypeItem")]
        public Guid? TypeId { get; set; }
        public virtual TypeItem TypeItem { get; set; }

        [ForeignKey("GenderItem")]
        public Guid? GenderId { get; set; }
        public virtual GenderItem GenderItem { get; set; }

        [ForeignKey("Origin")]
        public Guid? OriginId { get; set; }
        public virtual Origin Origin { get; set; }

        [ForeignKey("Location")]
        public Guid? LocationId { get; set; }
        public virtual Location Location { get; set; }
        
        public string Image { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public string Url { get; set; }
    }
}

