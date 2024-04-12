// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetTagByTitle;

/// <summary>
/// Handler, that gets a tag by given title from database.
/// </summary>
public class GetTagByTitleHandler : IRequestHandler<GetTagByTitleQuery, Result<TagDto>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetTagByTitleHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that get a tag from database with given title.
    /// </summary>
    /// <param name="request">
    /// Request witt title to find a tag.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A TagDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<TagDto>> Handle(GetTagByTitleQuery request, CancellationToken cancellationToken)
    {
        var tag = await _repositoryWrapper.TagRepository.GetFirstOrDefaultAsync(f => f.Title == request.Title);

        if (tag is null)
        {
            string errorMsg = string.Format(TagErrors.GetTagByTitleHandlerCanNotFindByTitleError, request.Title);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<TagDto>(tag));
    }
}
