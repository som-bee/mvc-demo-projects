using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp_CustomAuthFilter.Filter
{
    // or :ActionFilterAttribute
    public class CustomActionFilter : FilterAttribute, IActionFilter
    {
        // after executing action method and before showing the view 
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        //before executing action method
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}