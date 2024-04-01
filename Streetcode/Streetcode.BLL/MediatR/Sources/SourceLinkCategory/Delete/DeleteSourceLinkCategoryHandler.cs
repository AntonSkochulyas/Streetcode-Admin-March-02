using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete
{
    public class DeleteSourceLinkCategoryHandler : IRequestHandler<DeleteSourceLinkCategoryCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public DeleteSourceLinkCategoryHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteSourceLinkCategoryCommand request, CancellationToken cancellationToken)
        {
            int id = request.id;
            var sourceLinkCategory = await _repositoryWrapper.SourceCategoryRepository.GetFirstOrDefaultAsync(sc => sc.Id == id);
            if (sourceLinkCategory == null)
            {
                string errorMsg = $"No source link category found by entered Id - {id}";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcodeContents = await _repositoryWrapper.StreetcodeCategoryContentRepository.GetAllAsync(sc => sc.SourceLinkCategoryId == id);
            if (streetcodeContents is not null)
            {
                foreach (var item in streetcodeContents)
                {
                    _repositoryWrapper.StreetcodeCategoryContentRepository.Delete(item);
                }
            }

            _repositoryWrapper.SourceCategoryRepository.Delete(sourceLinkCategory);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Failed to delete the source link category";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
