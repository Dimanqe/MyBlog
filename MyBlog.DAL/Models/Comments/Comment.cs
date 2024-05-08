using MyBlog.DAL.Models.Articles;
using MyBlog.DAL.Models.Users;

namespace MyBlog.DAL.Models.Comments;

/// <summary>
///     Сущность комментария к статье
/// </summary>
public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Author { get; set; }

    public Article Article { get; set; }
    public int ArticleId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
}