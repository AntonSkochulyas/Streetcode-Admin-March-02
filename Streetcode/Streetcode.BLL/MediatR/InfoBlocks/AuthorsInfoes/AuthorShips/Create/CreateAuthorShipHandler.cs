// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Create
{
    /// <summary>
    /// Handler, that handles a process of creating an authroship.
    /// </summary>
    public class CreateAuthorShipHandler : IRequestHandler<CreateAuthorShipCommand, Result<AuthorShipDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateAuthorShipHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that create a new authorship.
        /// </summary>
        /// <param name="request">
        /// Request with new authorship.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation toke, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A AuthorShipDto, or error, if it was while creating process.
        /// </returns>
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
