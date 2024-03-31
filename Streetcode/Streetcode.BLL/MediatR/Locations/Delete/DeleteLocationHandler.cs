using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Locations.Delete
{
    public class DeleteLocationHandler : IRequestHandler<DeleteLocationCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteLocationHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            int id = request.id;

            var location = await _repositoryWrapper.LocationRepository.GetFirstOrDefaultAsync(n => n.Id == id);

            if (location == null)
            {
                string errorMsg = string.Format(LocationsErrors.DeleteLocationHandlerNotFoundByIdError, id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.LocationRepository.Delete(location);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = LocationsErrors.DeleteLocationHandlerCanNotDeleteLocationError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
