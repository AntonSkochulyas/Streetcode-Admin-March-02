using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Locations;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Locations;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Locations.Create
{
    public class CreateLocationHandler : IRequestHandler<CreateLocationCommand, Result<LocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateLocationHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<LocationDto>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var newLocation = _mapper.Map<Location>(request.newLocation);

            if (newLocation is null)
            {
                string errorMsg = LocationsErrors.CreateLocationHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.LocationRepository.Create(newLocation);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<LocationDto>(entity));
            }
            else
            {
                string errorMsg = LocationsErrors.CreateLocationHandlerCanNotCreateLocationError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
