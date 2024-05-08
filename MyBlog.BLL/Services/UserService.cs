using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Roles.Response;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.BLL.ViewModels.Users.Response;
using MyBlog.DAL.Models.Articles;
using MyBlog.DAL.Models.Roles;
using MyBlog.DAL.Models.Users;
using MyBlog.DAL.Repositories;
using MyBlog.DAL.Repositories.Interfaces;

namespace MyBlog.BLL.Services;

public class UserService : IUserService
{
    private readonly ArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleService;
    private readonly SignInManager<User> _signInService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userService;

    public UserService(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork,
        RoleManager<Role> roleService, SignInManager<User> signInManager)
    {
        _userService = userManager;
        _mapper = mapper;
        _roleService = roleService;
        _signInService = signInManager;
        _unitOfWork = unitOfWork;

        _articleRepository = (ArticleRepository)_unitOfWork.GetRepository<Article>();
    }

    public async Task<IdentityResult> CreateUserAsync(CreateUserViewModel model)
    {
        var user = new User();
        if (model.Firstname != null) user.Firstname = model.Firstname;
        if (model.Lastname != null) user.Lastname = model.Lastname;
        if (model.Email != null) user.Email = model.Email;
        if (model.Login != null) user.UserName = model.Login;
        var userRole = new Role { Name = "Пользователь", Description = "базовый уровень пользователя" };
        var result = await _userService.CreateAsync(user, model.Password);
        await _userService.AddToRoleAsync(user, userRole.Name);
        return result;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userService.FindByIdAsync(id.ToString());
        await _userService.DeleteAsync(user);
    }

    public async Task<IdentityResult> EditUserAsync(EditUserViewModel model)
    {
        var user = await _userService.FindByIdAsync(model.Id.ToString());

        if (model.Firstname != null) user.Firstname = model.Firstname;
        if (model.Lastname != null) user.Lastname = model.Lastname;
        if (model.Email != null) user.Email = model.Email;
        if (model.NewPassword != null)
            user.PasswordHash = _userService.PasswordHasher.HashPassword(user, model.NewPassword);
        if (model.Login != null) user.UserName = model.Login;
        foreach (var role in model.Roles)
        {
            var roleName = _roleService.FindByIdAsync(role.Id.ToString()).Result.Name;
            if (role.IsSelected)
                await _userService.AddToRoleAsync(user, roleName);
            else
                await _userService.RemoveFromRoleAsync(user, roleName);
        }

        if (!model.Roles.Any(x => x.IsSelected)) await _userService.AddToRoleAsync(user, "Пользователь");
        var result = await _userService.UpdateAsync(user);
        return result;
    }

    public async Task<EditUserViewModel> EditUserAsync(int id)
    {
        var user = await _userService.FindByIdAsync(id.ToString());
        var allRolesName = _roleService.Roles.ToList();
        var model = new EditUserViewModel
        {
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Login = user.UserName,
            Email = user.Email,
            NewPassword = string.Empty,
            Id = user.Id,
            Roles = allRolesName.Select(r => new RoleViewModel { Id = r.Id, Name = r.Name }).ToList()
        };
        return model;
    }

    public async Task<UserViewModel> GetUserAsync(int id)
    {
        var user = await _userService.FindByIdAsync(id.ToString());
        var userRoles = await _userService.GetRolesAsync(user);
        var userArticles = await _articleRepository.GetByUserId(user.Id);
        var userViewModel = new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.Firstname,
            LastName = user.Lastname,
            Roles = userRoles.ToList(),
            Articles = userArticles
        };

        return userViewModel;
    }

    public async Task<List<UserViewModel>> GetUsersAsync()
    {
        var users = _userService.Users
            .Include(u => u.Articles)
            .ToList();
        var models = new List<UserViewModel>();

        foreach (var user in users)
        {
            var useRroles = await _userService.GetRolesAsync(user);
            var roles = new List<string>();

            foreach (var role in useRroles) roles.Add(role);

            models.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Firstname,
                Roles = roles
            });
        }

        return models;
    }

    public async Task<SignInResult> LoginUserAsync(LoginUserViewModel model)
    {
        Console.WriteLine(model.Email);
        var user = await _userService.FindByEmailAsync(model.Email);
        if (user == null)
            return SignInResult.Failed;

        var result = await _signInService.PasswordSignInAsync(user, model.Password, true, false);
        return result;
    }

    public async Task LogoutUserAccount()
    {
        await _signInService.SignOutAsync();
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterUserViewModel model)
    {
        var user = _mapper.Map<User>(model);
        var result = await _userService.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userService.AddToRoleAsync(user, "Пользователь");
            await _signInService.SignInAsync(user, false);

            return result;
        }

        return result;
    }
}