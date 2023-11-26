using Demo.DAL.Entities;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole>roleManager, IMapper mapper)
        {
			_userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
		public async Task<IActionResult> Index()
		{
			//var users = Enumerable.Empty<ApplicationUser>().ToList();
			var users = await _userManager.Users.Select(user => new UserViewModel 
			{ 
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,	
				Roles = _userManager.GetRolesAsync(user).Result
			}).ToListAsync();

			//if (String.IsNullOrEmpty(SearchValue))
			//	users.AddRange(_userManager.Users);
			//else
			//	users.Add(await _userManager.FindByEmailAsync(SearchValue));
			
			//var mappedusers = _mapper.Map<List<ApplicationUser>, IEnumerable<UserViewModel>>(users);

			return View(users);
		}
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
				var user = _mapper.Map<UserViewModel, ApplicationUser>(model);
				//var user = new ApplicationUser()
				//{
				//	Id = model.Id,
				//	UserName = model.Email.Split('@')[0],
				//	Email = model.Email,
				//	PhoneNumber = model.PhoneNumber,
				//	IsAgree = model.IsAgree

				//};
				var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);

        }


        //public async Task<IActionResult> Create()
        //{
        //	ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
        //	return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        //{
        //	if (ModelState.IsValid) // Server Side Validation
        //	{
        //		employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "images");
        //		var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
        //		await _unitOfWork.EmployeeRepository.Add(mappedEmp);
        //		return RedirectToAction(nameof(Index));
        //	}
        //	return View(employeeVM);
        //}

        public async Task<IActionResult> Details([FromRoute] string id, string ViewName = "Details")
		{
			if (id == null)
				return NotFound();
			var User = await _userManager.FindByIdAsync(id);
			if (User == null)
				return NotFound();
			var mappeduser = _mapper.Map<ApplicationUser , UserViewModel>(User);
			return View(ViewName, mappeduser);
		}
		public async Task<IActionResult> Edit(string id)
		{
			return await Details(id, "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel updatedUser)
		{
			if (id != updatedUser.Id)
				return BadRequest();
			if (ModelState.IsValid) // Server Side Validation
			{
				try
				{
                    //var user = await _userManager.FindByIdAsync(id);
                    //user.Email = updatedUser.Email;
                    //user.PhoneNumber = updatedUser.PhoneNumber;
                    var user = _mapper.Map<UserViewModel, ApplicationUser>(updatedUser);
					user = await _userManager.FindByIdAsync(id);
					user.UserName = updatedUser.UserName;
					user.Email = updatedUser.Email;
					user.PhoneNumber = updatedUser.PhoneNumber;
					user.IsAgree = updatedUser.IsAgree;
					await _userManager.UpdateAsync(user);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception e)
				{
					ModelState.AddModelError(string.Empty, e.Message);
				}
			}
			return View(updatedUser);
		}
		public async Task<IActionResult> Delete(string id)
		{
			return await Details(id, "Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel deletedUser)
		{
			if (id != deletedUser.Id)
				return BadRequest();
			if (ModelState.IsValid) // Server Side Validation
			{
				try
				{
                    var user = _mapper.Map<UserViewModel, ApplicationUser>(deletedUser);
                    user = await _userManager.FindByIdAsync(id);
					await _userManager.DeleteAsync(user);
					return RedirectToAction(nameof(Index));

				}
				catch (Exception e)
				{
					ModelState.AddModelError(string.Empty, e.Message);
				}
			}
			return View(deletedUser);
		}

	}
}
