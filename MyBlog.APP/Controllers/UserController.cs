using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.DAL.Models.Users;
using NLog;

namespace MyBlog.App.Controllers
{    /// <summary>
     /// Контроллер регистрации пользователя
     /// </summary>
    public class UserController : Controller
    {
		private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(SignInManager<User> signInManager, IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [Route("User/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("User/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
					return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
					}
				}
            }
            return View(model);
        }

        [Route("User/Login")]
        [HttpGet]
        public IActionResult Login()
        {
			return View();
        }

        [Route("User/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.Succeeded)
                {
					return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
				}
			}
            return View(model);
        }

		[Route("User/Logout")]
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _userService.LogoutUserAccount();

			return RedirectToAction("Index", "Home");
		}

		[Route("User/Create")]
        [Authorize(Roles = "Администратор")]
        [HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[Route("User/Create")]
        [Authorize(Roles = "Администратор")]
        [HttpPost]
		public async Task<IActionResult> Create(CreateUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _userService.CreateUserAsync(model);

				if (result.Succeeded)
				{
					return RedirectToAction("GetAll", "User");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}

		[Route("User/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _userService.EditUserAsync(id);

			return View(model);
        }

		[Route("User/Edit")]
		[Authorize(Roles = "Администратор, Модератор")]
		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
				await _userService.EditUserAsync(model);

				return RedirectToAction("GetAll", "User");
            }
            else
            {
				return View(model);
            }
        }

        [Route("User/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsersAsync();

			return View(users);
        }

        [Route("User/Get")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _userService.GetUserAsync(id);

			return View(model);
        }

        [Route("User/Delete")]
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _userService.GetUserAsync(id);
            await _userService.DeleteUserAsync(id);

			return RedirectToAction("GetAll", "User");
        }
    }
}