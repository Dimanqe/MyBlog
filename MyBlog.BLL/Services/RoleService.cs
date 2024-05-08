using Microsoft.AspNetCore.Identity;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Roles.Request;
using MyBlog.DAL.Models.Roles;

namespace MyBlog.BLL.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService(RoleManager<Role> roleService)
    {
        _roleManager = roleService;
    }

    public async Task<int> CreateRoleAsync(CreateRoleViewModel model)
    {
        var role = new Role { Name = model.Name, Description = model.Description };
        await _roleManager.CreateAsync(role);
        //return role.Id;
        return 0;
    }

    public async Task EditRoleAsync(EditRoleViewModel model)
    {
        if (string.IsNullOrEmpty(model.Name) && model.Description == null)
            return;
        var role = await _roleManager.FindByIdAsync(model.Id.ToString());
        if (!string.IsNullOrEmpty(model.Name))
            role.Name = model.Name;
        if (model.Description != null)
            role.Description = model.Description;
        await _roleManager.UpdateAsync(role);
    }

    public async Task<Role?> GetRoleAsync(int id)
    {
        return await _roleManager.FindByIdAsync(id.ToString());
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return _roleManager.Roles.ToList();
    }

    public async Task DeleteRoleAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        await _roleManager.DeleteAsync(role);
    }
}