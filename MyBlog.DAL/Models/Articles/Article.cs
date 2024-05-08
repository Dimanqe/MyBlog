using MyBlog.DAL.Models.Comments;
using MyBlog.DAL.Models.Tags;
using MyBlog.DAL.Models.Users;

namespace MyBlog.DAL.Models.Articles;

/// <summary>
///     Сущность статьи
/// </summary>
public class Article
{
    public Article()
    {
        CreatedDate = DateTime.Now;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
    public List<Tag> Tags { get; set; }
    public List<Comment> Comments { get; set; }
}