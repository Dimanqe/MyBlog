using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Tags.Request;
using MyBlog.DAL.Models.Tags;

namespace MyBlog.App.Controllers;

[ApiController]
[Route("TagController")]
public class TagController : Controller
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [Route("Create")]
    [Authorize(Roles = "Администратор, Модератор")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateTagViewModel model)
    {
        model.Id = 0;
        await _tagService.CreateTagAsync(model);

        return StatusCode(201);
    }

    [Route("Edit")]
    [Authorize(Roles = "Администратор, Модератор")]
    [HttpPatch]
    public async Task<IActionResult> Edit([FromBody] EditTagViewModel model, int id)
    {
        await _tagService.UpdateTagAsync(model, id);

        return StatusCode(201);
    }

    [Route("Delete")]
    [Authorize(Roles = "Администратор, Модератор")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);
        await _tagService.DeleteTagAsync(tag);

        return StatusCode(201);
    }

    [Route("GetAll")]
    [HttpGet]
    public async Task<List<Tag>> GetAll()
    {
        var tags = await _tagService.GetAllTagsAsync();

        return tags;
    }

    [Route("Get")]
    [HttpGet]
    public async Task<Tag> Get(int id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);

        return tag;
    }
}