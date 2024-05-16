using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Tags.Request;
using NLog;
using NLog.Fluent;

namespace MyBlog.App.Controllers
{
    public class TagController : Controller
    {
		private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult Create()
        {
			return View();
        }

        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagId = _tagService.CreateTagAsync(model);

				return RedirectToAction("GetAll", "User");
            }
            else
            {
				return View(model);
            }
        }

        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var view = await _tagService.UpdateTagAsync(id);

			return View(view);
        }

        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                await _tagService.UpdateTagAsync(model, id);

				return RedirectToAction("GetAll", "Tag");
            }
            else
            {
				return View(model);
            }
        }

        [Route("Tag/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, bool isConfirm = true)
        {
            if (isConfirm)
                await Delete(id);

			return RedirectToAction("GetAll", "Tag");
        }

        [Route("Tag/Delete")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            await _tagService.DeleteTagAsync(tag);

			return RedirectToAction("GetAll", "Tag");
        }

        [Route("Tag/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagService.GetAllTagsAsync();

			return View(tags);
        }

        [Route("Tag/Details")]
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var tags = await _tagService.GetTagByIdAsync(id);

			return View(tags);
        }
    }
}
