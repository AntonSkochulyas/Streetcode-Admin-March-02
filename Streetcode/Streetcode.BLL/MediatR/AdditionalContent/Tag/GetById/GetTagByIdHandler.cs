// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetById;

/// <summary>
/// Handler, that gets a tag by id.
/// </summary>
public class GetTagByIdHandler : IRequestHandler<GetTagByIdQuery, Result<TagDto>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    public GetTagByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that get a tag by id.
    /// </summary>
    /// <param name="request">
    /// Request with id to find a tag.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token for cancelling operation if it needed.
    /// </param>
    /// <returns>
    /// A finded TagDto, or error, if it was while finding process.
    /// </returns>
    public async Task<Result<TagDto>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _repositoryWrapper.TagRepository.GetFirstOrDefaultAsync(f => f.Id == request.Id);

        if (tag is null)
        {
            string errorMsg = TagErrors.GetTagByIdHandlerCanNotFindByIdError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<TagDto>(tag));
    }
}
