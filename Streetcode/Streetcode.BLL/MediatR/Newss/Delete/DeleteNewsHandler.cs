// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a news.
    /// </summary>
    public class DeleteNewsHandler : IRequestHandler<DeleteNewsCommand, Result<Unit>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteNewsHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }


        /// <summary>
        /// Method, that deletes a news.
        /// </summary>
        /// <param name="request">
        /// Request with news id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Unit, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<Unit>> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            var news = await _repositoryWrapper.NewsRepository.GetFirstOrDefaultAsync(n => n.Id == id);
            if (news == null)
            {
                string errorMsg = string.Format(NewsErrors.DeleteNewsHandlerCanNotFindNewsWithGivenIdError, request.Id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            if (news.Image is not null)
            {
                _repositoryWrapper.ImageRepository.Delete(news.Image);
            }

            _repositoryWrapper.NewsRepository.Delete(news);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            if (resultIsSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = NewsErrors.DeleteNewsHandlerFailedToDeleteError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
