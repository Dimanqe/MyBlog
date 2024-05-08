using Microsoft.EntityFrameworkCore;
using MyBlog.DAL.Models.Articles;

namespace MyBlog.DAL.Repositories;

public class ArticleRepository : Repository<Article>
{
    public ArticleRepository(BlogDbContext context) : base(context)
    {
    }

    public override async Task<List<Article>> GetAllAsync()
    {
        return await Set.Include(x => x.User).Include(x => x.Comments).Include(x => x.Tags).ToListAsync();
    }

    public override async Task<Article?> GetByIdAsync(int id)
    {
        return await Set.Include(x => x.User).Include(x => x.Tags).Include(x => x.Comments).Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Article>> GetByUserId(int userId)
    {
        return await Set.Include(x => x.User).Include(x => x.Tags).Include(x => x.Comments)
            .Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<List<Article>> GetByTagId(int tagId)
    {
        return await Set.Include(x => x.Tags).Include(x => x.Comments)
            .SelectMany(a => a.Tags, (a, t) => new { Article = a, TagId = t.Id }).Where(x => x.TagId == tagId)
            .Select(x => x.Article).ToListAsync();
    }
}