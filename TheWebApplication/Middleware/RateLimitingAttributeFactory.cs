using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Middleware
{
	public class RateLimitingAttributeFactory : ActionFilterAttribute, IFilterFactory
    {
        public string Name { get; set; }
        public bool IsCreating { get; set; }
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var attribute = serviceProvider.GetService<RateLimitingAttribute>();

            if (attribute != null)
            {
                attribute.Name = Name;
                attribute.IsCreating = IsCreating;
            }

            return attribute;
        }
    }
}

