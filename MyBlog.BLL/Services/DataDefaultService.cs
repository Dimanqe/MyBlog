using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.DAL.Models.Roles;
using MyBlog.DAL.Models.Users;

namespace MyBlog.BLL.Services;

public class DataDefaultService : IDataDefaultService
{
    private readonly RoleManager<Role> _roleService;
    private readonly UserManager<User> _userService;
    public IMapper _mapper;

    public DataDefaultService(RoleManager<Role> roleService, UserManager<User> userService, IMapper mapper)
    {
        _roleService = roleService;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task SeedDefaultData()
    {
        var admin = new RegisterUserViewModel
        {
            Login = "Administrator",
            Email = "admin@gmail.com",
            Password = "qwerty",
            Firstname = "Administrator",
            Lastname = "Ivanov"
        };
        var moderator = new RegisterUserViewModel
        {
            Login = "Moderator",
            Email = "moderator@gmail.com",
            Password = "qwerty",
            Firstname = "Moder",
            Lastname = "Petrov"
        };
        var basicUser = new RegisterUserViewModel
        {
            Login = "User",
            Email = "user@gmail.com",
            Password = "qwerty",
            Firstname = "Ivan",
            Lastname = "Sidorov"
        };

        var user = _mapper.Map<User>(basicUser);
        var user1 = _mapper.Map<User>(moderator);
        var user2 = _mapper.Map<User>(admin);

        var userRole = new Role { Name = "Пользователь", Description = "базовый уровень пользователя" };
        var moderRole = new Role
            { Name = "Модератор", Description = "уровень модерирования" };
        var adminRole = new Role { Name = "Администратор", Description = "уровень администрирования" };

        if (_userService.Users.FirstOrDefault(x => x.UserName == "Moderator") == null)
        {
            await _userService.CreateAsync(user1, moderator.Password);
            if (_roleService.Roles.FirstOrDefault(x => x.Name == "Модератор") == null)
                await _roleService.CreateAsync(moderRole);
            await _userService.AddToRoleAsync(user1,
                _roleService.Roles.FirstOrDefault(x => x.Name == "Модератор").Name);
        }

        if (_userService.Users.FirstOrDefault(x => x.UserName == "Administrator") == null)
        {
            await _userService.CreateAsync(user2, admin.Password);
            if (_roleService.Roles.FirstOrDefault(x => x.Name == "Администратор") == null)
                await _roleService.CreateAsync(adminRole);
            await _userService.AddToRoleAsync(user2,
                _roleService.Roles.FirstOrDefault(x => x.Name == "Администратор").Name);
        }

        if (_userService.Users.FirstOrDefault(x => x.UserName == "User") == null)
        {
            await _userService.CreateAsync(user, basicUser.Password);
            if (_roleService.Roles.FirstOrDefault(x => x.Name == "Пользователь") == null)
                await _roleService.CreateAsync(userRole);
            await _userService.AddToRoleAsync(user,
                _roleService.Roles.FirstOrDefault(x => x.Name == "Пользователь").Name);
        }
    }
}