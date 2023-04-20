using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Location")]
    public class Location : EntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public Location(string name, string url) : base()
        {
            Name = name;
            Url = url;
        }
    }
}

