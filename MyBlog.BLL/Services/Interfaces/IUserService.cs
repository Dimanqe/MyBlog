using Microsoft.AspNetCore.Identity;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.BLL.ViewModels.Users.Response;

namespace MyBlog.BLL.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(RegisterUserViewModel model);

    Task<IdentityResult> CreateUserAsync(CreateUserViewModel model);

    Task<SignInResult> LoginUserAsync(LoginUserViewModel model);

    Task<IdentityResult> EditUserAsync(EditUserViewModel model);

    Task<EditUserViewModel> EditUserAsync(int id);

    Task DeleteUserAsync(int id);

    Task<List<UserViewModel>> GetUsersAsync();

    Task<UserViewModel> GetUserAsync(int id);

    Task LogoutUserAccount();
}