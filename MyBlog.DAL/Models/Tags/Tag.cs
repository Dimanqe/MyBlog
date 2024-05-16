using MyBlog.DAL.Models.Articles;

namespace MyBlog.DAL.Models.Tags;

/// <summary>
///     Сущность тега
/// </summary>
public class Tag
{
    public Tag(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public List<Article>? Articles { get; set; }
}