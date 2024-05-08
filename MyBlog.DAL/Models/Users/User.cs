using Microsoft.AspNetCore.Identity;
using MyBlog.DAL.Models.Articles;
using MyBlog.DAL.Models.Comments;

namespace MyBlog.DAL.Models.Users;

/// <summary>
///     Сущность пользователя
/// </summary>
public class User : IdentityUser<int>
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;

    public List<Article> Articles { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}