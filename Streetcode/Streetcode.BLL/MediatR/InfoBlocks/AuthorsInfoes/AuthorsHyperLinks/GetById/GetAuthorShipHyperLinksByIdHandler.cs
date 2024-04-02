using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById
{
    public class GetAuthorShipHyperLinksByIdHandler : IRequestHandler<GetAuthorShipHyperLinksByIdQuery, Result<AuthorShipHyperLinkDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAuthorShipHyperLinksByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

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
