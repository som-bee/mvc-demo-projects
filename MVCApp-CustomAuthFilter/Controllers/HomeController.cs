using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp_CustomAuthFilter.Filter;

namespace MVCApp_CustomAuthFilter.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [CustomAuthentication]
        [CustomActionFilter]
        [CustomExceptionFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}