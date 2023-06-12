using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic; 

namespace MVCAPP_Dep_Injection.Controllers
{
    public class HomeController : Controller
    {
        private IEmployee _employee = null;

        public HomeController(IEmployee employee)
        {
            _employee = employee;
        }
    
        public ActionResult Index()
        {
            int count = _employee.getTotalEmployees();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}