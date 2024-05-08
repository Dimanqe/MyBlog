using Microsoft.EntityFrameworkCore;
using MyBlog.DAL.Models.Tags;

namespace MyBlog.DAL.Repositories;

public class TagRepository : Repository<Tag>
{
    public TagRepository(BlogDbContext context) : base(context)
    {
    }

    public override async Task<List<Tag>> GetAllAsync()
    {
        return await Set.Include(x => x.Articles).ToListAsync();
    }

    public override async Task<Tag?> GetByIdAsync(int id)
    {
        return await Set.Include(x => x.Articles).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Tag>> GetByArticleIdAsync(int articleId)
    {
        return await Set.Include(x => x.Articles)
            .SelectMany(x => x.Articles, (x, a) => new { Tag = x, ArticleId = a.Id })
            .Where(x => x.ArticleId == articleId).Select(x => x.Tag).ToListAsync();
    }
}