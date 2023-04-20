using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SpeciesItem")]
    public class SpeciesItem : EntityBase
    {
        public string Species { get; set; }
    }
}

