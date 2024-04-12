﻿// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting an article by given id.
    /// </summary>
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, Result<Unit>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Constructor
        public DeleteArticleHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that delete an article by given id.
        /// </summary>
        /// <param name="request">
        /// Request with article id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Unit, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<Unit>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            var article = await _repositoryWrapper.ArticleRepository.GetFirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                string errorMsg = $"No article found by entered Id - {id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.ArticleRepository.Delete(article);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Failed to delete article";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
