using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MVCApp_CustomAuthFilter.Filter
{
    public class CustomAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {

        //authentication logics goes here
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //checking whether the user logged in or not
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if not then setting the result as unauthorized
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        //after checking authentication performing desired operations 
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //if result is null/unauthorized
            if(filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult) {
                //returning error view as result
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
        }
    }
}