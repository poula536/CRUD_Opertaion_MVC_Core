using Demo.BLL.Interfaces;
using Demo.BLL.Mock_Repositories;
using Demo.BLL.Repositories;
using Demo.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    // Inheritance : FullTimeEmployee Is a Employee
    // Composition : Room Has a chair
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            // 1-ViewData ---> Transfer Data from controller to action
            //ViewData["Message"] = "Welcome Dude";
            // 2- ViewBag ---> Transfer Data from controller to action
            //ViewBag.Message = "Welcome ViewBag";

            // 3-TempData ---> Transfer Data From Action to Action


            var departments = await _departmentRepository.GetAll();
            return View(departments);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                await _departmentRepository.Add(department);
                TempData["Message"] = "Department is created Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Details([FromRoute] int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();// Helper Method  return --> error 404
            var department = await _departmentRepository.Get(id.Value);
            if (department == null)
                return BadRequest();
            return View(ViewName, department);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ///if (id == null)
            ///    return BadRequest();
            ///var department = _departmentRepository.Get(id.Value);
            ///if (department == null)
            ///    return BadRequest();
            ///return View(department);
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    await _departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View(department);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    await _departmentRepository.Delete(department);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View(department);
        }
    }
}
