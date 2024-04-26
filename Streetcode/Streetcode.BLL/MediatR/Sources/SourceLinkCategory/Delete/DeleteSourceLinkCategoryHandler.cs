// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a source link category.
    /// </summary>
    public class DeleteSourceLinkCategoryHandler : IRequestHandler<DeleteSourceLinkCategoryCommand, Result<SourceLinkResponseDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Mapper
        private readonly IMapper _mapper;

        // Parametric constructor
        public DeleteSourceLinkCategoryHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that deletes a source link category.
        /// </summary>
        /// <param name="request">
        /// Request with source link category id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A SourceLinkResponseDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<SourceLinkResponseDto>> Handle(DeleteSourceLinkCategoryCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            var sourceLinkCategory = await _repositoryWrapper.SourceCategoryRepository.GetItemBySpecAsync(new GetByIdSourceLinkCategorySpec(request.Id));
            if (sourceLinkCategory == null)
            {
                string errorMsg = string.Format(SourceErrors.DeleteSourceLinkCategoryHandlerCanNotFindSourceLinkCategoryWithGivenIdError, id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.SourceCategoryRepository.Delete(sourceLinkCategory);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<SourceLinkResponseDto>(sourceLinkCategory));
            }
            else
            {
                string errorMsg = SourceErrors.DeleteSourceLinkCategoryHandlerFailedToDeleteError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
