// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Dto.Streetcode.TextContent;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessaru namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a related term.
    /// </summary>
    public class DeleteRelatedTermHandler : IRequestHandler<DeleteRelatedTermCommand, Result<RelatedTermDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repository;

        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteRelatedTermHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that deletes a related term.
        /// </summary>
        /// <param name="request">
        /// Request with related term word to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A RelatedTermDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<RelatedTermDto>> Handle(DeleteRelatedTermCommand request, CancellationToken cancellationToken)
        {
            var relatedTerm = await _repository.RelatedTermRepository.GetFirstOrDefaultAsync(rt => rt.Word.ToLower().Equals(request.Word.ToLower()));

            if (relatedTerm is null)
            {
                string errorMsg = string.Format(StreetcodeErrors.DeleteRelatedTermHandlerCannotFindRelatedTermError, request.Word);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            _repository.RelatedTermRepository.Delete(relatedTerm);

            var resultIsSuccess = await _repository.SaveChangesAsync() > 0;
            var relatedTermDto = _mapper.Map<RelatedTermDto>(relatedTerm);
            if(resultIsSuccess && relatedTermDto != null)
            {
                return Result.Ok(relatedTermDto);
            }
            else
            {
                string errorMsg = StreetcodeErrors.DeleteRelatedTermHandlerFailedToDeleteRelatedTermError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
