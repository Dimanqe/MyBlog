using MyBlog.DAL.Models.Articles;

namespace MyBlog.BLL.ViewModels.Users.Response;

/// <summary>
///     Модель пользователя
/// </summary>
public class UserViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string? LastName { get; set; }
    public string UserName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Photo { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public List<Article> Articles { get; set; }
}