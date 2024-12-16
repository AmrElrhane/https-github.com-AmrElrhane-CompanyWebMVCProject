using Company.Data.Entities;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Employee.Dto;
using Company.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Company.Web.Controllers
{
    [Authorize]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService , IDepartmentService departmentService )
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Index(string searchInp)
        {
            //ViewBag.Message = "Hello From EmployeeDto Index (ViewBag)";

            //ViewData["TextMessage"] = "Hello From EmployeeDto Index (ViewData)";

            //TempData["TextTempMessage"] = "Hello From EmployeeDto Index (TempData)";

            if (string.IsNullOrEmpty(searchInp))
            {
                var employee = _employeeService.GetAll();
                return View(employee);
            }
            else
            {
                var employee = _employeeService.GetEmployeeByName(searchInp);
                return View(employee);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employee);
                    return RedirectToAction(nameof(Index));
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                return View(employee);
            }
        }


        //public IActionResult Details(int? id, string viewname = "Details")
        //{


        //}
        //[HttpGet]
        //public IActionResult Update(int? id)
        //{


        //}
        //[HttpPost]
        //public IActionResult Update(int? id, Department department)
        //{

        //}
        //public IActionResult Delete(int id)
        //{

        //}
    }
}
