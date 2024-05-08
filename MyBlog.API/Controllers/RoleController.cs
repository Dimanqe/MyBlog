using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Roles.Request;
using MyBlog.DAL.Models.Roles;

namespace MyBlog.App.Controllers;

[ApiController]
[Route("RoleController")]
public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [Route("Create")]
    [Authorize(Roles = "Администратор, Модератор")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleViewModel model)
    {
        var result = await _roleService.CreateRoleAsync(model);
        return StatusCode(201);
    }

    [Route("Edit")]
    [Authorize(Roles = "Администратор, Модератор")]
    [HttpPatch]
    public async Task<IActionResult> Edit(EditRoleViewModel model)
    {
        await _roleService.EditRoleAsync(model);
        return StatusCode(201);
    }

    [Route("Delete")]
    [Authorize(Roles = "Администратор, Модератор")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleService.DeleteRoleAsync(id);

        return StatusCode(201);
    }

    [Route("GetAll")]
    [HttpGet]
    public async Task<List<Role>> GetAll()
    {
        var roles = await _roleService.GetRolesAsync();

        return roles;
    }
}