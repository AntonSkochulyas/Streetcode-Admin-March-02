// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all authorships from database.
    /// </summary>
    public class GetAllAuthorShipsHandler : IRequestHandler<GetAllAuthorShipsQuery, Result<IEnumerable<AuthorShipDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public GetAllAuthorShipsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all authorships from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all authorships from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of AuthorShipDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<AuthorShipDto>>> Handle(GetAllAuthorShipsQuery request, CancellationToken cancellationToken)
        {
            var authorShip = await _repositoryWrapper.AuthorShipRepository.GetAllAsync();

            if (authorShip is null)
            {
                const string errorMsg = $"Cannot find any authorship";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<AuthorShipDto>>(authorShip));
        }
    }
}
