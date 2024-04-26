// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Sources.StreetcodeCategoryContent;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a streetcode category content.
    /// </summary>
    public class DeleteStreetcodeCategoryContentHandler : IRequestHandler<DeleteStreetcodeCategoryContentCommand, Result<StreetcodeCategoryContentDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Mapper
        private readonly IMapper _mapper;

        // Parametric constructor
        public DeleteStreetcodeCategoryContentHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that deletes a streetcode category content.
        /// </summary>
        /// <param name="request">
        /// Request with streetcode category content id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A StreetcodeCategoryContentDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<StreetcodeCategoryContentDto>> Handle(DeleteStreetcodeCategoryContentCommand request, CancellationToken cancellationToken)
        {
            int sourceLinkCategoryId = request.sourceLinkCategoryId;
            int streetcodeId = request.streetcodeId;
            var streetcodeCategoryContent = await _repositoryWrapper.StreetcodeCategoryContentRepository
                .GetItemBySpecAsync(new GetByStreetcodeIdStreetcodeCategoryContentSpec(streetcodeId, sourceLinkCategoryId));
            if (streetcodeCategoryContent == null)
            {
                string errorMsg = string.Format(
                    SourceErrors.DeleteStreetcodeCategoryContentHandlerCanNotFindStreetcodeCategoryContentWithGivenIdError, sourceLinkCategoryId, streetcodeId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.StreetcodeCategoryContentRepository.Delete(streetcodeCategoryContent);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<StreetcodeCategoryContentDto>(streetcodeCategoryContent));
            }
            else
            {
                string errorMsg = SourceErrors.DeleteStreetcodeCategoryContentHandlerFailedToDeleteError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
