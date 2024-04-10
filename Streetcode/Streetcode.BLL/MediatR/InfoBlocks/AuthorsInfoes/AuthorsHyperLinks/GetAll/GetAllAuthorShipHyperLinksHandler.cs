// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all of author ship hyperlinks from database.
    /// </summary>
    public class GetAllAuthorShipHyperLinksHandler : IRequestHandler<GetAllAuthorShipHyperLinksQuery, Result<IEnumerable<AuthorShipHyperLinkDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllAuthorShipHyperLinksHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets all authorship hyperlinks from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all authorship hyperlinks from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of AuthorShipHyperLinkDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<AuthorShipHyperLinkDto>>> Handle(GetAllAuthorShipHyperLinksQuery request, CancellationToken cancellationToken)
        {
            var authorsHyperLinks = await _repositoryWrapper.AuthorShipHyperLinkRepository.GetAllAsync();

            if (authorsHyperLinks is null)
            {
                const string errorMsg = $"Cannot find any authors hyper links";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<AuthorShipHyperLinkDto>>(authorsHyperLinks));
        }
    }
}
