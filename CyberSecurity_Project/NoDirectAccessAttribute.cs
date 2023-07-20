using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberSecurity_Project
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //it can be null if someone opened a browser and just entered your site address
            if (filterContext.HttpContext.Request.UrlReferrer == null)
            {
                //The result that is returned by the action method.
                filterContext.Result = new RedirectResult("https://localhost:44312/User/fail");
            }
        }
    }
}