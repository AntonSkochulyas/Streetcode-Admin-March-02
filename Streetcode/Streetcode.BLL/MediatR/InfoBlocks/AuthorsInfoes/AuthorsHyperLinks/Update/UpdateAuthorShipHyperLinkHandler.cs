// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update
{
    /// <summary>
    /// Handler, that handles a process of updating an authorship hyperlink.
    /// </summary>
    public class UpdateAuthorShipHyperLinkHandler : IRequestHandler<UpdateAuthorShipHyperLinkCommand, Result<AuthorShipHyperLinkDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public UpdateAuthorShipHyperLinkHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that updates an authorship hyperlink.
        /// </summary>
        /// <param name="request">
        /// Request with updated authorship hyperlink.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A AuthorShipHyperLinkDto, or error, if it was while updating process.
        /// </returns>
        public async Task<Result<AuthorShipHyperLinkDto>> Handle(UpdateAuthorShipHyperLinkCommand request, CancellationToken cancellationToken)
        {
            var authorHyperLink = _mapper.Map<AuthorShipHyperLink>(request.AuthorsHyperLink);

            if (authorHyperLink is null)
            {
                const string errorMsg = $"Cannot convert null to author hyper link";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            var response = _mapper.Map<AuthorShipHyperLinkDto>(authorHyperLink);

            _repositoryWrapper.AuthorShipHyperLinkRepository.Update(authorHyperLink);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                const string errorMsg = $"Failed to update author hyper link";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
