using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete
{
    public class DeleteInfoBlockHandler : IRequestHandler<DeleteInfoBlockCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteInfoBlockHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteInfoBlockCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            var infoBlock = await _repositoryWrapper.InfoBlockRepository.GetFirstOrDefaultAsync(n => n.Id == id);

            if (infoBlock == null)
            {
                string errorMsg = $"No info block found by entered Id - {id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.InfoBlockRepository.Delete(infoBlock);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Failed to delete info block";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
