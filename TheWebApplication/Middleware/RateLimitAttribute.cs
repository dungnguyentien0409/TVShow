using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TheWebApplication.Middleware
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RateLimitAttribute : ActionFilterAttribute
    {
        public int Seconds { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /*
            var key = string.Format("{0}-{1}-{2}",
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                filterContext.ActionDescriptor.ActionName,
                filterContext.HttpContext.Request.UserHostAddress
            );*/
            var allowExecute = true;
            var tmp = filterContext.HttpContext.Request;

            /*
            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key,
                    true,
                    null,
                    DateTime.Now.AddSeconds(Seconds),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Low,
                    null);
                allowExecute = true;
            }*/

            if (!allowExecute)
            {
                filterContext.Result = new ContentResult
                {
                    Content = string.Format("You can call this every {0} seconds", Seconds)
                };
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
        }
    }
}

