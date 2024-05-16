using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Roles.Request;
using NLog;
using NLog.Fluent;

namespace MyBlog.App.Controllers
{
	public class RoleController : Controller
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[Route("Role/Create")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[Route("Role/Create")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Create(CreateRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var roleId = await _roleService.CreateRoleAsync(model);

				return RedirectToAction("GetAll", "Role");
			}
			else
			{
				ModelState.AddModelError("", "Некорректные данные");

				return View(model);
			}
		}

		[Route("Role/Edit")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var role = _roleService.GetRoleAsync(id);
			var view = new EditRoleViewModel { Id = id, Description = role.Result?.Description, Name = role.Result?.Name };

			return View(view);
		}

		[Route("Role/Edit")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Edit(EditRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _roleService.EditRoleAsync(model);

				return RedirectToAction("GetAll", "Role");
			}
			else
			{
				ModelState.AddModelError("", "Некорректные данные");

				return View(model);
			}
		}

		[Route("Role/Delete")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpGet]
		public async Task<IActionResult> Delete(int id, bool isConfirm = true)
		{
			if (isConfirm)
				await Delete(id);

			return RedirectToAction("GetAll", "Role");
		}

		[Route("Role/Delete")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _roleService.DeleteRoleAsync(id);

			return RedirectToAction("GetAll", "Role");
		}

		[Route("Role/GetAll")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var roles = await _roleService.GetRolesAsync();
 
			return View(roles);
		}
	}
}
