using System.ComponentModel.DataAnnotations;
using MyBlog.DAL.Models.Tags;

namespace MyBlog.BLL.ViewModels.Tags.Response;

/// <summary>
///     Модель одного тега
/// </summary>
public class TagsViewModel
{
 public List<Tag> Tags { get; set; }
}