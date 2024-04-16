using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.RelatedFigure;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Delete;

public class DeleteRelatedFigureHandler : IRequestHandler<DeleteRelatedFigureCommand, Result<RelatedFigureDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;

    public DeleteRelatedFigureHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<RelatedFigureDto>> Handle(DeleteRelatedFigureCommand request, CancellationToken cancellationToken)
    {
        var relation = await _repositoryWrapper.RelatedFigureRepository
                                .GetFirstOrDefaultAsync(rel =>
                                rel.ObserverId == request.ObserverId &&
                                rel.TargetId == request.TargetId);

        if (relation is null)
        {
            string errorMsg = string.Format(StreetcodeErrors.DeleteRelatedFigureHandlerCannotFindRelationBetweenStreetcodesWithCorrespondingIdsError, request.ObserverId, request.TargetId);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        _repositoryWrapper.RelatedFigureRepository.Delete(relation);

        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
        if(resultIsSuccess)
        {
            return Result.Ok(_mapper.Map<RelatedFigureDto>(relation));
        }
        else
        {
            string errorMsg = StreetcodeErrors.DeleteRelatedFigureHandlerFailedToDeleteError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}
