using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Department.Dto;
using Company.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            //TempData.Keep("TextTempMessage");
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDto deparmtent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(deparmtent);

                    TempData["TextTempMessage"] = "Hello From Employee Index (TempData)";

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("DepartmentError", "ValidationErrors");
                return View(deparmtent);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(deparmtent);
            }


        }


        public IActionResult Details(int? id, string viewname = "Details")
        {

            var department = _departmentService.GetById(id);
            if (department is null)
            {
                return RedirectToAction("Not Found Page", null, "Home");
            }
            return View(viewname, department);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");

        }
        [HttpPost]
        public IActionResult Update(int? id, DepartmentDto department)
        {
            if (department.Id != id.Value)
                return RedirectToAction("Not Found Page", null, "Home");
            _departmentService.Update(department);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetById(id);
            if (department is null)
            {
                return RedirectToAction("Not Found Page", null, "Home");
            }
            _departmentService.Delete(department);
            return RedirectToAction(nameof(Index));
        }
    }
}
