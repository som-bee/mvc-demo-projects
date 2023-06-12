using Microsoft.AspNetCore.Mvc;
using MVCApp.Model;
using MVCAppWithDB.Models;
using System.Diagnostics;
using MVCApp.DB.DBOperations;

namespace MVCAppWithDB.Controllers
{
    public class HomeController : Controller
    {
        EmployeeRepository employeeRepository = null;

        private readonly ILogger<HomeController> _logger;

        public HomeController()
        {
            employeeRepository = new EmployeeRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                int id = employeeRepository.AddEmployee(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.Result = "New Employee added successfully...";
                }
            }




            return View();
        }

        [HttpGet]
        public IActionResult GetAllEmployeeRecords() {


            var result = employeeRepository.GetAllEmployees();
            return View(result);

        }
        public IActionResult EmployeeDetails(int id)
        {
            var employeeDetails = employeeRepository.GetEmployee(id);
            return View(employeeDetails);
        }
        public IActionResult DeleteEmployee(int id)
        {
            var result = employeeRepository.DeleteEmployee(id);
            return RedirectToAction("GetAllEmployeeRecords");
        }

        public IActionResult EditEmployee(int id)
        {
            var employee = employeeRepository.GetEmployee(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeModel model) { 
        
            if(ModelState.IsValid){
                var result = employeeRepository.UpdateEmployee(model.Id,model);
                return RedirectToAction("GetAllEmployeeRecords");
            }
           
            return View();
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}