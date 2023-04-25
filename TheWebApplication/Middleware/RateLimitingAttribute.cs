using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Middleware
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RateLimitingAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _config;
        private static IRateLimitingCache _rateLimitingCache { get; } =
            new RateLimitingCache(new MemoryCache(new MemoryCacheOptions()));
        public string Name { get; set; }
        public bool IsCreating { get; set; }

        public RateLimitingAttribute(IConfiguration config)
        {
            _config = config;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var key = GetKey(Name, filterContext);
            if (IsCreating && _rateLimitingCache.IsInCache(key))
            {
                _rateLimitingCache.RemoveFromCache(key);
                return;
            }

            var minutes = _config.GetValue<int>("RateLimiting:GetCharacteristic");
            var allowExecute = _rateLimitingCache.AddToCache(key, minutes);
            if (!allowExecute)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
        }

        private string GetKey(string name, ActionExecutingContext filterContext)
        {
            return string.Format("{0}-{1}", Name, filterContext.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);
        }
    }
}

