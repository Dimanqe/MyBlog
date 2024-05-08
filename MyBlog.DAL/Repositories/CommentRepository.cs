using Microsoft.EntityFrameworkCore;
using MyBlog.DAL.Models.Comments;

namespace MyBlog.DAL.Repositories;

public class CommentRepository : Repository<Comment>
{
    public CommentRepository(BlogDbContext context) : base(context)
    {
    }

    public override async Task<List<Comment>> GetAllAsync()
    {
        return await Set.Include(x => x.Article).Include(x => x.User).ToListAsync();
    }

    public async Task<List<Comment>> GetByArticleIdAsync(int articleId)
    {
        return await Set.Include(x => x.Article).Include(x => x.User).Where(x => x.ArticleId == articleId)
            .ToListAsync();
    }

    //public async Task<List<Comment>> GetByUserIdAsync(int userId)
    //{
    //    return await Set.Include(x => x.Article).Include(x => x.User).Where(x => x.UserId == userId).ToListAsync();
    //}
}