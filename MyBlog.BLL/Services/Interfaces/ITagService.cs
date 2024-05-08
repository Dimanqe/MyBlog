using MyBlog.BLL.ViewModels.Tags.Request;
using MyBlog.DAL.Models.Tags;

namespace MyBlog.BLL.Services.Interfaces;

public interface ITagService
{
    /// <summary>
    ///     Метода создания тега
    /// </summary>
    Task<int> CreateTagAsync(CreateTagViewModel model);

    /// <summary>
    ///     Метод получения списка всех тегов
    /// </summary>
    Task<List<Tag>> GetAllTagsAsync();

    /// <summary>
    ///     Метод получения тега по идентификатору
    /// </summary>
    Task<Tag> GetTagByIdAsync(int id);

    /// <summary>
    ///     Метод обновления тега
    /// </summary>
    Task UpdateTagAsync(EditTagViewModel model, int id);

    /// <summary>
    ///     Метод обновления тега
    /// </summary>
    Task<EditTagViewModel> UpdateTagAsync(int id);

    /// <summary>
    ///     Метод удаления тега
    /// </summary>
    Task DeleteTagAsync(Tag tag);
}