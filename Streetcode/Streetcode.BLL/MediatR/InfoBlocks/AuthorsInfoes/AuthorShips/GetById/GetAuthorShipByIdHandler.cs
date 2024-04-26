// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetById
{
    /// <summary>
    /// handler, that handles a process of getiing authorship by given id.
    /// </summary>
    public class GetAuthorShipByIdHandler : IRequestHandler<GetAuthorShipByIdQuery, Result<AuthorShipDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAuthorShipByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets authorship by given id.
        /// </summary>
        /// <param name="request">
        /// Request with authorship id to get.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A AuthorShipDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<AuthorShipDto>> Handle(GetAuthorShipByIdQuery request, CancellationToken cancellationToken)
        {
            var authorShip = await _repositoryWrapper.AuthorShipRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

            if (authorShip is null)
            {
                string errorMsg = $"Cannot find an authorship with corresponding id: {request.Id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<AuthorShipDto>(authorShip));
        }
    }
}
