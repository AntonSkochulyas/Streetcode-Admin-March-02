// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new streetcode category content.
    /// </summary>
    public class CreateStreetcodeCategoryContentHandler : IRequestHandler<CreateStreetcodeCategoryContentCommand, Result<CategoryContentCreatedDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateStreetcodeCategoryContentHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new streetcode category content.
        /// </summary>
        /// <param name="request">
        /// Request with a new streetcode category content.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A CategoryContentCreatedDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<CategoryContentCreatedDto>> Handle(CreateStreetcodeCategoryContentCommand request, CancellationToken cancellationToken)
        {
            var newStreetcodeCategoryContent = _mapper.Map<DAL.Entities.Sources.StreetcodeCategoryContent>(request.StreetcodeCategoryContentDto);
            if (newStreetcodeCategoryContent is null)
            {
                string errorMsg = SourceErrors.CreateStreetcodeCategoryContentHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcode = await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.StreetcodeCategoryContentDto.StreetcodeId);

            if (streetcode is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.CreateStreetcodeCategoryContentCommandValidatorStreetcodeIdIsRequiredError,
                    request.StreetcodeCategoryContentDto.StreetcodeId);

                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var sourceLink = await _repositoryWrapper.SourceCategoryRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.StreetcodeCategoryContentDto.SourceLinkCategoryId);

            if (sourceLink is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.CreateStreetcodeCategoryContentCommandValidatorSourceLinkIdIsRequiredError,
                    request.StreetcodeCategoryContentDto.SourceLinkCategoryId);

                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.StreetcodeCategoryContentRepository.Create(newStreetcodeCategoryContent);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<CategoryContentCreatedDto>(entity));
            }
            else
            {
                string errorMsg = SourceErrors.CreateStreetcodeCategoryContentHandlerFailedToCreateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
