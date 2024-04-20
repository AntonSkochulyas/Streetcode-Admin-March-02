// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.RelatedFigure;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a related figure.
    /// </summary>
    public class DeleteRelatedFigureHandler : IRequestHandler<DeleteRelatedFigureCommand, Result<RelatedFigureDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteRelatedFigureHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that deletes a related figure.
        /// </summary>
        /// <param name="request">
        /// Request with related figure id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A RelatedFigureDto, or error, if it was while deleting process.
        /// </returns>
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
            if (resultIsSuccess)
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
}
