// Necessary usings.
using System.Linq.Expressions;
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetSortedByStartTitle;

/// <summary>
/// Handler, that sorts a tags by start title.
/// </summary>
public class GetSortedTagsByStartTitleHandler : IRequestHandler<GetSortedTagsByStartTitleHandlerQuery, Result<IEnumerable<TagDto>>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetSortedTagsByStartTitleHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that sorts a tags by start title. 
    /// </summary>
    /// <param name="request">
    /// Request with start title to sort.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token for cancelling operation.
    /// </param>
    /// <returns>
    /// A IEnumerable of TagDto, or error, if it was while sorting process.
    /// </returns>
    public async Task<Result<IEnumerable<TagDto>>> Handle(GetSortedTagsByStartTitleHandlerQuery request, CancellationToken cancellationToken)
    {
        if (request.Take < 1)
        {
            string errorMsg = "Take can not be less than 1";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var sortExpression = new Dictionary<Expression<Func<DAL.Entities.AdditionalContent.Tag, object>>, SortDirection>
        {
            { t => t.Title, SortDirection.Ascending }
        };

        IQueryable<DAL.Entities.AdditionalContent.Tag>? tags = null;

        if (request.StartsWithTitle != string.Empty)
        {
            tags = _repositoryWrapper.TagRepository
            .Get(
                take: request.Take,
                orderBy: sortExpression,
                predicate: t => t.Title.StartsWith(request.StartsWithTitle));
        }
        else
        {
            tags = _repositoryWrapper.TagRepository
            .Get(
                take: request.Take,
                orderBy: sortExpression);
        }

        if (tags is null)
        {
            string errorMsg = TagErrors.GetAllTagsHandlerCanNotFindAnyTagsError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<IEnumerable<TagDto>>(tags));
    }
}
