using Microsoft.AspNetCore.Mvc;
using MVCAppJsonAjax.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MVCAppJsonAjax.Controllers
    
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            throw new Exception("custom excepon");
              
            return View();
        }
        [Route("home/getjson")]
        public JsonResult GetJson()
        {
            Student student = new Student()
            {
                Id = 1,
                Name = "Test",
            };
            

            var jsonObj = JsonConvert.SerializeObject(student);
            return Json(jsonObj);
        }
        [HttpPost]
        public JsonResult PostJson(Student student)
        {
            Thread.Sleep(1000);
            var JsonObj = JsonConvert.SerializeObject(student);
            return Json(JsonObj);
        }

        public JsonResult Countries()
        {
            var countries = new List<string>()
            {
                "India",
                "Bangladesh",
                "Nepal"
            };
            var json = JsonConvert.SerializeObject(countries);
            Thread.Sleep(1000);
            return Json(json);
        }
        public JsonResult States()
        {
            var states = new List<string>()
            {
                "state -- India",
                "state -- Bangladesh",
                "state -- Nepal"
            };
            var json = JsonConvert.SerializeObject(states);
            Thread.Sleep(1000);
            return Json(json);
        }
        public JsonResult Cities()
        {
            var cities = new List<string>()
            {
                "citiy -- India",
                "city -- Bangladesh",
                "city -- Nepal"
            };
            var json = JsonConvert.SerializeObject(cities);
            Thread.Sleep(1000);
            return Json(json);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}