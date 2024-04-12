using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Create
{
    public class CreateAuthorShipHandler : IRequestHandler<CreateAuthorShipCommand, Result<AuthorShipDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateAuthorShipHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<AuthorShipDto>> Handle(CreateAuthorShipCommand request, CancellationToken cancellationToken)
        {
            var newAuthorShip = _mapper.Map<AuthorShip>(request.NewAuthorShip);

            if (newAuthorShip is null)
            {
                const string errorMsg = "Cannot convert null to authorship";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.AuthorShipRepository.Create(newAuthorShip);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<AuthorShipDto>(entity));
            }
            else
            {
                const string errorMsg = "Failed to create an authorship";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
