using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.DAL.Models.Users;

namespace MyBlog.App.Controllers
{
	public class ArticleController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly UserManager<User> _userService;

		public ArticleController(IArticleService articleService, UserManager<User> userService)
		{
			_articleService = articleService;
			_userService = userService;
		}

		[Route("Article/Get")]
		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			var article = await _articleService.GetArticleAsync(id);
			return View(article);
		}

		[Route("Article/Create")]
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Create()
		{
			var model = await _articleService.CreateArticleAsync();


			return View(model);
		}

		[Route("Article/Create")]
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(CreateArticleViewModel model)
		{
            var user = await _userService.FindByNameAsync(User?.Identity?.Name);
			model.AuthorId = user.Id;
			if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
			{
				ModelState.AddModelError("", "Напишите название и тело статьи");

				return View(model);
			}
			await _articleService.CreateArticleAsync(model);

			return RedirectToAction("GetAll", "Article");
		}

		[Route("Article/Edit")]
		[HttpGet]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Edit(int id)
		{
			var model = await _articleService.UpdateArticleAsync(id);

			return View(model);
		}

		[Route("Article/Edit")]
		[HttpPost]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Edit(EditArticleViewModel model, int Id)
		{
			if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Content))
			{
				ModelState.AddModelError("", "Не все поля заполненны");

				return View(model);
			}
			await _articleService.UpdateArticleAsync(model, Id);

			return RedirectToAction("GetAll", "Article");
		}

        [HttpGet]
		[Route("Article/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Delete(int id, bool confirm = true)
		{
			if (confirm)
				await Delete(id);

			return RedirectToAction("GetAll", "Article");
		}

		[HttpPost]
		[Route("Article/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> Delete(int id)
		{
			await _articleService.DeleteArticleAsync(id);

			return RedirectToAction("GetAll", "Article");
		}

		[HttpGet]
		[Route("Article/GetAll")]
		public async Task<IActionResult> GetAll()
		{
			var articles = await _articleService.GetAllArticlesAsync();

			return View(articles);
		}
	}
}
