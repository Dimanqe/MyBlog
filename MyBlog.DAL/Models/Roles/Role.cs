using Microsoft.AspNetCore.Identity;

namespace MyBlog.DAL.Models.Roles;

/// <summary>
///     Сущность роли пользователя
/// </summary>
public class Role : IdentityRole<int>
{
    public string? Description { get; set; }
}