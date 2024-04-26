// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new source link.
    /// </summary>
    public class CreateSourceLinkCategoryHandler : IRequestHandler<CreateSourceLinkCategoryCommand, Result<SourceLinkCategoryDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateSourceLinkCategoryHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new source link.
        /// </summary>
        /// <param name="request">
        /// Request with a new source link.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A SourceLinkDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<SourceLinkCategoryDto>> Handle(CreateSourceLinkCategoryCommand request, CancellationToken cancellationToken)
        {
            var newSourceLinkCategory = _mapper.Map<DAL.Entities.Sources.SourceLinkCategory>(request.SourceLinkCategoryContentDto);
            if (newSourceLinkCategory is null)
            {
                string errorMsg = SourceErrors.CreateSourceLinkCategoryHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            if (newSourceLinkCategory.Title is null)
            {
                string errorMsg = SourceErrors.UpdateSourceLinkCategoryCommandValidatorTitleIsRequiredError;
                _logger.LogError(newSourceLinkCategory, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var image = await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(
                x => x.Id == newSourceLinkCategory.ImageId);

            if (image is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.UpdateSourceLinkHandlerCanNotFindImageWithGivenIdError,
                    request.SourceLinkCategoryContentDto.ImageId);

                _logger.LogError(newSourceLinkCategory, errorMsg);
                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.SourceCategoryRepository.Create(newSourceLinkCategory);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<SourceLinkCategoryDto>(entity));
            }
            else
            {
                string errorMsg = SourceErrors.CreateSourceLinkCategoryHandlerFailedToCreateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}