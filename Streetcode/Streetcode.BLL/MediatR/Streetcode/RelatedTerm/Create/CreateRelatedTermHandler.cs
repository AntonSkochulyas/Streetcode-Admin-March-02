// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Entity = Streetcode.DAL.Entities.Streetcode.TextContent.RelatedTerm;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new related term.
    /// </summary>
    public class CreateRelatedTermHandler : IRequestHandler<CreateRelatedTermCommand, Result<RelatedTermDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repository;

        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateRelatedTermHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new related term.
        /// </summary>
        /// <param name="request">
        /// Request with a new related term.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A RelatedTermDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<RelatedTermDto>> Handle(CreateRelatedTermCommand request, CancellationToken cancellationToken)
        {
            var relatedTerm = _mapper.Map<Entity>(request.RelatedTerm);

            if (relatedTerm is null)
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotCreateNewRelatedWordForTermError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var existingTerms = await _repository.RelatedTermRepository
                .GetAllAsync(
                predicate: rt => rt.TermId == request.RelatedTerm.TermId && rt.Word == request.RelatedTerm.Word);

            if (existingTerms is null || existingTerms.Any())
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerWordWithThisDefinitionAlreadyExistsError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var createdRelatedTerm = _repository.RelatedTermRepository.Create(relatedTerm);

            var isSuccessResult = await _repository.SaveChangesAsync() > 0;

            if(!isSuccessResult)
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotSaveChangesInDatabaseAfterRelatedWordCreationError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var createdRelatedTermDTO = _mapper.Map<RelatedTermDto>(createdRelatedTerm);

            if(createdRelatedTermDTO != null)
            {
                return Result.Ok(createdRelatedTermDTO);
            }
            else
            {
                string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotMapEntityError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
