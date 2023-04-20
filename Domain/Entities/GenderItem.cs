using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("GenderItem")]
    public class GenderItem : EntityBase
    {
        public string Gender { get; set; }

        public GenderItem(string gender) : base()
        {
            Gender = gender;
        }
    }
}

