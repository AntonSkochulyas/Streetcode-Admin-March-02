using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetSortedByStartTitle;

public class GetSortedTagsByStartTitleHandler : IRequestHandler<GetSortedTagsByStartTitleHandlerQuery, Result<IEnumerable<TagDto>>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetSortedTagsByStartTitleHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

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
