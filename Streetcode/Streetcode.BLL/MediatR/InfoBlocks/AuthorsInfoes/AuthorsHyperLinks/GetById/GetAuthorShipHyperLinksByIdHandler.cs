// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById
{
    /// <summary>
    /// Handler, that handles a process of getting authorship hyperlink by given id.
    /// </summary>
    public class GetAuthorShipHyperLinksByIdHandler : IRequestHandler<GetAuthorShipHyperLinksByIdQuery, Result<AuthorShipHyperLinkDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAuthorShipHyperLinksByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets an authorship hyperlink by given id.
        /// </summary>
        /// <param name="request">
        /// Authorship hyperlink id to get.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A AuthorShipHyperLinkDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<AuthorShipHyperLinkDto>> Handle(GetAuthorShipHyperLinksByIdQuery request, CancellationToken cancellationToken)
        {
            var authorsHyperLink = await _repositoryWrapper.AuthorShipHyperLinkRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

            if (authorsHyperLink is null)
            {
                string errorMsg = $"Cannot find an authors hyper link with corresponding id: {request.Id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<AuthorShipHyperLinkDto>(authorsHyperLink));
        }
    }
}
