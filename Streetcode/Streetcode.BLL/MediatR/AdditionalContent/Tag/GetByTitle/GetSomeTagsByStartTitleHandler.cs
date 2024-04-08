using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetAll;

public class GetSomeTagsByStartTitleHandler : IRequestHandler<GetSomeTagsByStartTitleHandlerQuery, Result<IEnumerable<TagDto>>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetSomeTagsByStartTitleHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<TagDto>>> Handle(GetSomeTagsByStartTitleHandlerQuery request, CancellationToken cancellationToken)
    {
        var tags = await _repositoryWrapper.TagRepository.GetAllAsync();

        if(request.Title == string.Empty)
        {
            tags = tags.Where(t => t.Title.StartsWith(request.Title));
        }

        tags = tags.OrderBy(t => t.Title)
            .Take(request.Take);

        if (tags is null)
        {
            string errorMsg = TagErrors.GetAllTagsHandlerCanNotFindAnyTagsError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
    }
}
