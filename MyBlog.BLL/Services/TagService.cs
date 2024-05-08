using AutoMapper;
using MyBlog.BLL.Services.Interfaces;
using MyBlog.BLL.ViewModels.Tags.Request;
using MyBlog.DAL.Models.Tags;
using MyBlog.DAL.Repositories;
using MyBlog.DAL.Repositories.Interfaces;

namespace MyBlog.BLL.Services;

public class TagService : ITagService
{
    private readonly IMapper _mapper;
    private readonly TagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TagService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

        _tagRepository = (TagRepository)_unitOfWork.GetRepository<Tag>();
    }

    public async Task<int> CreateTagAsync(CreateTagViewModel model)
    {
        var tag = _mapper.Map<Tag>(model);
        await _tagRepository.CreateAsync(tag);
        return tag.Id;
    }

    public async Task DeleteTagAsync(Tag tag)
    {
        await _tagRepository.DeleteAsync(tag);
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _tagRepository.GetAllAsync();
    }

    public async Task<Tag> GetTagByIdAsync(int id)
    {
        return await _tagRepository.GetByIdAsync(id);
    }

    public async Task UpdateTagAsync(EditTagViewModel model, int id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);
        tag.Name = model.Name;
        await _tagRepository.UpdateAsync(tag);
    }

    public async Task<EditTagViewModel> UpdateTagAsync(int id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);
        var result = new EditTagViewModel
        {
            Name = tag.Name
        };
        return result;
    }

    public async Task DeleteTagByIdAsync(int id)
    {
        var tag = await GetTagByIdAsync(id);
        if (tag == null) return;

        await _tagRepository.DeleteAsync(tag);
    }
}