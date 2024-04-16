using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Update;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Delete
{
    public class DeleteStreetcodeCategoryContentHandler : IRequestHandler<DeleteStreetcodeCategoryContentCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public DeleteStreetcodeCategoryContentHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteStreetcodeCategoryContentCommand request, CancellationToken cancellationToken)
        {
            int sourceLinkCategoryId = request.sourceLinkCategoryId;
            int streetcodeId = request.streetcodeId;
            var streetcodeCategoryContent = await _repositoryWrapper.StreetcodeCategoryContentRepository
                .GetFirstOrDefaultAsync(sc => sc.SourceLinkCategoryId == sourceLinkCategoryId && sc.StreetcodeId == streetcodeId);
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
                return Result.Ok(Unit.Value);
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
