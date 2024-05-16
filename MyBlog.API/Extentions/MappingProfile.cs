using AutoMapper;
using MyBlog.BLL.ViewModels.Articles.Request;
using MyBlog.BLL.ViewModels.Articles.Response;
using MyBlog.BLL.ViewModels.Comments;
using MyBlog.BLL.ViewModels.Tags.Request;
using MyBlog.BLL.ViewModels.Users.Request;
using MyBlog.BLL.ViewModels.Users.Response;
using MyBlog.DAL.Models.Articles;
using MyBlog.DAL.Models.Comments;
using MyBlog.DAL.Models.Tags;
using MyBlog.DAL.Models.Users;

namespace MyBlog.API.Extentions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterUserViewModel, User>()
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
        CreateMap<EditUserViewModel, User>();
        CreateMap<UserViewModel, User>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));
        CreateMap<User, UserViewModel>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));
        CreateMap<CreateCommentViewModel, Comment>();
        CreateMap<EditCommentViewModel, Comment>();
        CreateMap<CreateArticleViewModel, Article>();
        CreateMap<ArticleViewModel, Article>();
        CreateMap<EditArticleViewModel, Article>();
        CreateMap<CreateTagViewModel, Tag>();
        CreateMap<EditTagViewModel, Tag>();
    }
}