using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Origin")]
    public class Origin : EntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public Origin(string name, string url) : base()
        {
            Name = name;
            Url = url;
        }
    }
}

