using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.BLL.ViewModels.Users.Response;

namespace MyBlog.API.Controllers;

[ApiController]
[Route("UserController")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> Create(RegisterUserViewModel model)
    {
        var result = await _userService.RegisterUserAsync(model);

        return StatusCode(result.Succeeded ? 201 : 204);
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginUserViewModel model)
    {
        var result = await _userService.LoginUserAsync(model);

        return StatusCode(result.Succeeded ? 201 : 204);
    }

    [Route("Logout")]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutUserAccount();

        return StatusCode(201);
    }

    [Route("Edit")]
    [Authorize(Roles = "Администратор, Модератор")]
    [HttpPatch]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        var result = await _userService.EditUserAsync(model);

        return StatusCode(result.Succeeded ? 201 : 204);
    }

    [Route("GetAll")]
    [HttpGet]
    public async Task<List<UserViewModel>> GetAll()
    {
        var users = await _userService.GetUsersAsync();

        return users;
    }

    [Route("Get")]
    [HttpGet]
    public async Task<UserViewModel> Get(int id)
    {
        var user = await _userService.GetUserAsync(id);

        return user;
    }

    [Route("Delete")]
    [Authorize(Roles = "Администратор")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteUserAsync(id);

        return StatusCode(201);
    }
}