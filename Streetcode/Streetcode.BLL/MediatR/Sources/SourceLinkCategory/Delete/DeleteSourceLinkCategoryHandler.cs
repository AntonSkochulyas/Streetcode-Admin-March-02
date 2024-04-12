using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
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
            int id = request.Id;
            var sourceLinkCategory = await _repositoryWrapper.SourceCategoryRepository.GetFirstOrDefaultAsync(sc => sc.Id == id);
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
                return Result.Ok(Unit.Value);
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
