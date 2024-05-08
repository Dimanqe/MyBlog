using MyBlog.BLL.ViewModels.Roles.Request;
using MyBlog.DAL.Models.Roles;

namespace MyBlog.BLL.Services.Interfaces;

public interface IRoleService
{
    Task<int> CreateRoleAsync(CreateRoleViewModel model);

    Task EditRoleAsync(EditRoleViewModel model);

    Task DeleteRoleAsync(int id);

    Task<List<Role>> GetRolesAsync();

    Task<Role?> GetRoleAsync(int id);
}