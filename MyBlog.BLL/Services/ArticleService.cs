using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.BLL.ViewModels.Articles.Response;
using MyBlog.BLL.ViewModels.Tags.Response;
using MyBlog.DAL.Models.Articles;
using MyBlog.DAL.Models.Comments;
using MyBlog.DAL.Models.Tags;
using MyBlog.DAL.Models.Users;
using MyBlog.DAL.Repositories;
using MyBlog.DAL.Repositories.Interfaces;

namespace MyBlog.BLL.Services;

public class ArticleService : IArticleService
{
    private readonly ArticleRepository _articleRepository;
    private readonly CommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly TagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userService;


    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userService)
    {
        _userService = userService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;

        _articleRepository = (ArticleRepository)_unitOfWork.GetRepository<Article>();
        _commentRepository = (CommentRepository)_unitOfWork.GetRepository<Comment>();
        _tagRepository = (TagRepository)_unitOfWork.GetRepository<Tag>();
    }

    public async Task<CreateArticleViewModel> CreateArticleAsync()
    {
        var article = new Article();

        var tags = (await _tagRepository.GetAllAsync())
            .Select(t => new TagViewModel { Id = t.Id, Name = t.Name }).ToList();

        var model = new CreateArticleViewModel
        {
            Title = article.Title = string.Empty,
            Content = article.Content = string.Empty,
            Tags = tags
        };

        return model;
    }

    public async Task<int> CreateArticleAsync(CreateArticleViewModel model)
    {
        var tags = new List<Tag>();

        if (model.Tags != null)
        {
            var articleTags = model.Tags.Where(t => t.IsSelected).ToList();
            var tagsId = articleTags.Select(t => t.Id).ToList();
            tags = (await _tagRepository.GetAllAsync()).Where(t => tagsId.Contains(t.Id)).ToList();
        }

        var article = new Article
        {
            Id = model.Id,
            Title = model.Title,
            Content = model.Content,
            Tags = tags,
            UserId = model.AuthorId
        };

        var user = await _userService.FindByIdAsync(model.AuthorId.ToString());
        user.Articles.Add(article);

        await _articleRepository.CreateAsync(article);
        await _userService.UpdateAsync(user);

        return article.Id;
    }

    public async Task DeleteArticleAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        await _articleRepository.DeleteAsync(article);
    }

    public async Task<ArticlesViewModel> GetAllArticlesAsync()
    {
        var articlesModel = new ArticlesViewModel();
        articlesModel.Articles = await _articleRepository.GetAllAsync();
        return articlesModel;
    }

    public async Task<ArticleViewModel> GetArticleAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        var user = await _userService.FindByIdAsync(article.UserId.ToString());
        var comments = _commentRepository.GetByArticleIdAsync(article.Id);
        article.Id = id;
        foreach (var comment in await comments)
            if (article.Comments.FirstOrDefault(c => c.Id == comment.Id) == null)
                article.Comments.Add(comment);
        if (user.Id != null)
        {
            //article.UserId = user.Id;
        }

        var articleModel = new ArticleViewModel
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            CreatedDate = article.CreatedDate,
            AuthorId = article.UserId,
            AuthorName = article.User.UserName,
            Tags = article.Tags,
            Comments = article.Comments
        };

        return articleModel;
    }

    public async Task<EditArticleViewModel> UpdateArticleAsync(int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        var tags = (await _tagRepository.GetAllAsync()).Select(t => new TagViewModel { Id = t.Id, Name = t.Name })
            .ToList();
        foreach (var tag in tags)
            if (tags != null)
                foreach (var articleTag in article.Tags)
                {
                    if (articleTag.Id != tag.Id) continue;
                    tag.IsSelected = true;
                    break;
                }

        var model = new EditArticleViewModel
        {
            Id = id,
            Title = article.Title,
            Content = article.Content,
            Tags = tags
        };
        return model;
    }

    public async Task UpdateArticleAsync(EditArticleViewModel model, int id)
    {
        var article = await _articleRepository.GetByIdAsync(id);
        article.Title = model.Title;
        article.Content = model.Content;
        foreach (var tag in model.Tags)
        {
            var tagChanged = await _tagRepository.GetByIdAsync(tag.Id);
            if (tag.IsSelected)
                article.Tags.Add(tagChanged);
            else
                article.Tags.Remove(tagChanged);
        }

        await _articleRepository.UpdateAsync(article);
    }
}