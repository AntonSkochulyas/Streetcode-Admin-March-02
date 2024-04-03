using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete
{
    public class DeleteAuthorShipHyperLinkHandler : IRequestHandler<DeleteAuthorShipHyperLinkCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteAuthorShipHyperLinkHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteAuthorShipHyperLinkCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            var authorsHyperLink = await _repositoryWrapper.AuthorShipHyperLinkRepository.GetFirstOrDefaultAsync(a => a.Id == id);

            if (authorsHyperLink == null)
            {
                string errorMsg = $"No authors hyper link found by entered Id - {id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.AuthorShipHyperLinkRepository.Delete(authorsHyperLink);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Failed to delete authors hyper link";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
