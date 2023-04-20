using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TypeItem")]
    public class TypeItem : EntityBase
    {
        public string Type { get; set; }
    }
}

