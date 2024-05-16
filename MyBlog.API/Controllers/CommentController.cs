using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Comments;
using MyBlog.DAL.Models.Comments;
using MyBlog.DAL.Models.Users;

namespace MyBlog.API.Controllers;

[ApiController]
[Route("CommentController")]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly UserManager<User> _userManager;

    public CommentController(ICommentService commentService, UserManager<User> userManager)
    {
        _commentService = commentService;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("Create")]
    [Authorize]
    public async Task<IActionResult> CreateComment(CreateCommentViewModel model, int articleId)
    {
        model.ArticleId = articleId;
        var user = await _userManager.FindByNameAsync(User?.Identity?.Name);
        var comment = _commentService.CreateCommentAsync(model, user.Id);

        return StatusCode(201);
    }

    [Route("Edit")]
    [HttpPatch]
    [Authorize(Roles = "Администратор, Модератор")]
    public async Task<IActionResult> Edit(EditCommentViewModel model)
    {
        await _commentService.EditCommentAsync(model, model.Id);

        return StatusCode(201);
    }

    [Route("GetAll")]
    [HttpPatch]
    [Authorize(Roles = "Администратор, Модератор")]
    public async Task<List<Comment>> GetAll()
    {
        var result = await _commentService.GetCommentsAsync();

        return result;
    }

    [HttpDelete]
    [Route("Delete")]
    [Authorize(Roles = "Администратор, Модератор")]
    public async Task<IActionResult> Delete(int id)
    {
        await _commentService.DeleteCommentAsync(id);

        return StatusCode(201);
    }
}