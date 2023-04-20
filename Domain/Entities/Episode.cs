using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Episode")]
    public class Episode : EntityBase
    {
        [ForeignKey("Characteristic")]
        public Guid CharacteristicId { get; set; }
        public Characteristic Characteristic { get; set; }
        
        public string EpisodeUrl { get; set; }
    }
}

