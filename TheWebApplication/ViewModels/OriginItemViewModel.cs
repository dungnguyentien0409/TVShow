using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class OriginItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}

