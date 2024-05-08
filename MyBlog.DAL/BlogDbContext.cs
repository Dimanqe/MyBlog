using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.DAL.Models.Articles;
using MyBlog.DAL.Models.Comments;
using MyBlog.DAL.Models.Roles;
using MyBlog.DAL.Models.Tags;
using MyBlog.DAL.Models.Users;

namespace MyBlog.DAL;

public class BlogDbContext : IdentityDbContext<User, Role, int>
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Comment>()
            .ToTable("Comments")
            .HasOne(a => a.User)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.UserId)
            .HasPrincipalKey(d => d.Id)
            .IsRequired(false);
    }
}