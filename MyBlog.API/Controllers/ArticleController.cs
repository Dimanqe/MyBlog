using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.BLL.ViewModels.Articles.Response;
using MyBlog.DAL.Models.Users;

namespace MyBlog.App.Controllers;

[ApiController]
[Route("ArticleController")]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly UserManager<User> _userService;

    public ArticleController(IArticleService articleService, UserManager<User> userService)
    {
        _articleService = articleService;
        _userService = userService;
    }

    [Route("Get")]
    [HttpGet]
    public async Task<ArticleViewModel> Get(int id)
    {
        var result = await _articleService.GetArticleAsync(id);
        return result;
    }

    [Route("Create")]
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateArticleViewModel model)
    {
        var user = await _userService.FindByNameAsync(User?.Identity?.Name);
        model.AuthorId = user.Id;
        await _articleService.CreateArticleAsync(model);

        return StatusCode(201);
    }

    [Route("Edit")]
    [HttpPatch]
    [Authorize(Roles = "Администратор, Модератор")]
    public async Task<IActionResult> Edit(EditArticleViewModel model, int Id)
    {
        await _articleService.UpdateArticleAsync(model, Id);

        return StatusCode(201);
    }

    [HttpPost]
    [Route("Article/Delete")]
    [Authorize(Roles = "Администратор, Модератор")]
    public async Task<IActionResult> Delete(int id)
    {
        await _articleService.DeleteArticleAsync(id);

        return StatusCode(201);
    }

    [HttpGet]
    [Route("Article/GetAll")]
    public async Task<ArticlesViewModel> GetAll()
    {
        var articles = await _articleService.GetAllArticlesAsync();

        return articles;
    }
}