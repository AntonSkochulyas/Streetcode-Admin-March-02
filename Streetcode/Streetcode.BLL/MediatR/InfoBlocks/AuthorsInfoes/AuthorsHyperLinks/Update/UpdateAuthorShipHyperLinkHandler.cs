using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update
{
    public class UpdateAuthorShipHyperLinkHandler : IRequestHandler<UpdateAuthorShipHyperLinkCommand, Result<AuthorShipHyperLinkDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public UpdateAuthorShipHyperLinkHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

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
