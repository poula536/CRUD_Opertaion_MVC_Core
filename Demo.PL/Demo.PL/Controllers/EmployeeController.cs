using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Specifications;
using Demo.DAL.Entities;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //[AllowAnonymous]
        public async Task<IActionResult> Index(string SearchValue)
        {
            var Employees = Enumerable.Empty<Employee>();
            if (String.IsNullOrEmpty(SearchValue))
            {
                var spec = new EmployeeWithDepartmentSpecification();
                 Employees = await _unitOfWork.EmployeeRepository.GetAllWithSpecAsync(spec);
            }
            else
            {
                var spec = new EmployeeWithDepartmentSpecification(SearchValue);
                 Employees = _unitOfWork.EmployeeRepository.GetEmployeesByNameWithSpec(spec);
            }
            var mappdedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(mappdedEmp);

        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                ///Manual Mapping
                ///var mappedEmp = new Employee()
                ///{ 
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Adsress = employeeVM.Adsress,
                ///    Salary = employeeVM.Salary,
                ///    IsActive = employeeVM.IsActive,
                ///    DepartmentId = employeeVM.DepartmentId,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///};

                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.Add(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Details([FromRoute] int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();// Helper Method  return --> error 404
            var Employee = await _unitOfWork.EmployeeRepository.Get(id.Value);
            if (Employee == null)
                return BadRequest();
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(ViewName, mappedEmp);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ///if (id == null)
            ///    return BadRequest();
            ///var Employee = _employeeRepository.Get(id.Value);
            ///if (Employee == null)
            ///    return BadRequest();
            ///return View(Employee);
            ///
            //ViewBag.Departments = _departmentrepository.GetAll();
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    if (employeeVM.ImageName != null)
                    {
                        DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                        employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");
                    }
                    var mappedEmp = _mapper.Map<EmployeeViewModel , Employee>(employeeVM);
                    await _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    int count = await _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                    if(count > 0)
                        DocumentSettings.DeleteFile(employeeVM.ImageName, "images");
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }
            return View(employeeVM);
        }
    }
}
